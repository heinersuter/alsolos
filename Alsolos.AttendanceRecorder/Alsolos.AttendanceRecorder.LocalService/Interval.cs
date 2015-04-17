namespace Alsolos.AttendanceRecorder.LocalService
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Serialization;
    using Alsolos.AttendanceRecorder.WebApi.Controllers;

    [Serializable]
    [DataContract]
    public class Interval : IInterval
    {
        [XmlIgnore]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public IntervalState State { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [XmlIgnore]
        [DataMember]
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
        [DataMember]
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

        [DataMember]
        public string TimeAccountName { get; set; }

        [DataMember]
        public DateTime LastModified { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", State, Date, Start, End);
        }
    }
}
