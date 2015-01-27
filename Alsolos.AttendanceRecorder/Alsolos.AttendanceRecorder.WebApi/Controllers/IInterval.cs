namespace Alsolos.AttendanceRecorder.WebApi.Controllers
{
    using System;
    using System.Xml.Serialization;

    public interface IInterval
    {
        int Id { get; set; }

        IntervalState State { get; set; }

        DateTime Date { get; set; }

        [XmlIgnore]
        TimeSpan End { get; set; }

        DateTime LastModified { get; set; }

        string TimeAccountName { get; set; }
    }
}
