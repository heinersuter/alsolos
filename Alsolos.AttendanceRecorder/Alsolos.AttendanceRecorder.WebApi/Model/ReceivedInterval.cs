namespace Alsolos.AttendanceRecorder.WebApi.Model
{
    using System;

    public class ReceivedInterval : IInterval
    {
        public int Id { get; set; }

        public IntervalState State { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public DateTime LastModified { get; set; }

        public string TimeAccountName { get; set; }
    }
}
