namespace Alsolos.AttendanceRecorder.LocalService
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    public class TempStore
    {
        private readonly string _file;

        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(List<Interval>));

        public TempStore()
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            dir = Path.Combine(dir, "..\\AttendanceRecorder");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            this._file = Path.Combine(dir, "intervals.xml");
        }

        public void Save(List<Interval> intervals)
        {
            using (var stream = new FileStream(_file, FileMode.Create, FileAccess.Write))
            {
                _serializer.Serialize(stream, intervals);
            }
        }

        public List<Interval> Load()
        {
            if (!File.Exists(_file))
            {
                return new List<Interval>();
            }
            using (var stream = new FileStream(_file, FileMode.Open, FileAccess.Read))
            {
                var obj = _serializer.Deserialize(stream);
                return obj as List<Interval>;
            }
        }
    }
}
