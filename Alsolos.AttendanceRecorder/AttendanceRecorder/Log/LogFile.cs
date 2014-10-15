namespace AttendanceRecorder.Log
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Xml;
    using System.Xml.Serialization;
    using AttendanceRecorder.Model;

    public class LogFile
    {
        private readonly string _workingFilename;
        private readonly string _backupFilename;
        private bool _errorWhenReadingFile;

        public LogFile()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) || Debugger.IsAttached)
            {
                _workingFilename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AttendanceRecorder\Events.V1.dev.xml";
                _backupFilename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AttendanceRecorder\Events.V1.dev.backup.xml";
            }
            else
            {
                _workingFilename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AttendanceRecorder\Events.V1.xml";
                _backupFilename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AttendanceRecorder\Events.V1.backup.xml";
            }

            Load();
            EventCollection.SaveRequested += OnEventCollectionSaveRequested;
        }

        public EventCollection EventCollection { get; private set; }

        private void OnEventCollectionSaveRequested(object sender, EventArgs e)
        {
            var directoryName = Path.GetDirectoryName(_workingFilename);
            if (directoryName == null)
            {
                return;
            }
            Directory.CreateDirectory(directoryName);
            Save();
        }

        private void Load()
        {
            _errorWhenReadingFile = true;
            if (File.Exists(_workingFilename))
            {
                var serializer = new XmlSerializer(typeof(EventCollection));
                using (var reader = new XmlTextReader(_workingFilename))
                {
                    EventCollection = (EventCollection)serializer.Deserialize(reader);
                }
            }
            else
            {
                EventCollection = new EventCollection();
            }
            _errorWhenReadingFile = false;
        }

        private void Save()
        {
            if (_errorWhenReadingFile)
            {
                return;
            }

            if (File.Exists(_workingFilename))
            {
                File.Copy(_workingFilename, _backupFilename, true);
            }

            var serializer = new XmlSerializer(typeof(EventCollection));

            using (var writer = new XmlTextWriter(_workingFilename, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, EventCollection);
            }
        }
    }
}
