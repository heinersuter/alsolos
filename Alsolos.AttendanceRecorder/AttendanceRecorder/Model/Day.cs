namespace AttendanceRecorder.Model
{
    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;

    [Serializable]
    public class Day
    {
        [XmlAttribute]
        public DateTime Date { get; set; }

        public ObservableCollection<EventItem> Events { get; set; }

        public Day()
        {
            this.Events = new ObservableCollection<EventItem>();
        }
    }
}
