namespace Alsolos.AttendanceRecorder.LocalService
{
    using System;
    using System.ComponentModel;
    using System.Xml;
    using System.Xml.Serialization;
    using Alsolos.AttendanceRecorder.WebApi.Controllers;

    [Serializable]
    public class Interval : IInterval
    {
        public int Id { get; set; }

        public IntervalState State { get; set; }

        public DateTime Date { get; set; }

        [XmlIgnore]
        public TimeSpan Start { get; set; }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Start")]
        public string StartXmlString
        {
            get
            {
                return XmlConvert.ToString(Start);
            }

            set
            {
                Start = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        [XmlIgnore]
        public TimeSpan End { get; set; }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "End")]
        public string EndXmlString
        {
            get
            {
                return XmlConvert.ToString(End);
            }

            set
            {
                End = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        public string TimeAccountName { get; set; }

        public DateTime LastModified { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", State, Date, Start, End);
        }
    }
}
