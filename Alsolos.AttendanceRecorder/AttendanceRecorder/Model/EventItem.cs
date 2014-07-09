namespace AttendanceRecorder.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class EventItem
    {
        [XmlAttribute]
        public DateTime Time { get; set; }

        [XmlAttribute]
        public SystemState OldState { get; set; }

        [XmlAttribute]
        public SystemState NewState { get; set; }
    }
}
