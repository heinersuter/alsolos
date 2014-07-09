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
        private readonly string _filename;

        public EventCollection EventCollection { get; private set; }

        public LogFile()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()) || Debugger.IsAttached)
            {
                _filename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AttendanceRecorder\Events.V1.dev.xml";
            }
            else
            {
                _filename = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AttendanceRecorder\Events.V1.xml";
            }

            Load(_filename);
            EventCollection.SaveRequested += OnEventCollectionSaveRequested;
        }

        private void OnEventCollectionSaveRequested(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filename));
            this.Save(_filename);
        }

        private void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                var serializer = new XmlSerializer(typeof(EventCollection));
                using (var reader = new XmlTextReader(fileName))
                {
                    EventCollection = (EventCollection)serializer.Deserialize(reader);
                }
            }
            else
            {
                EventCollection = new EventCollection();
            }
        }

        private void Save(string fileName)
        {
            var serializer = new XmlSerializer(typeof(EventCollection));

            using (var writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, EventCollection);
            }
        }
    }
}
