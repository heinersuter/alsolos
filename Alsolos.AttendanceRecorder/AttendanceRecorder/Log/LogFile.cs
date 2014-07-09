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

        public EventCollection EventCollection { get; private set; }

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

        private void OnEventCollectionSaveRequested(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_workingFilename));
            Save();
        }

        private void Load()
        {
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
        }

        private void Save()
        {
            File.Copy(_workingFilename, _backupFilename, true);

            var serializer = new XmlSerializer(typeof(EventCollection));

            using (var writer = new XmlTextWriter(_workingFilename, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, EventCollection);
            }
        }
    }
}
