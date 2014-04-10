using System;

namespace Alsolos.AttendanceRecorder.LocalService {
    [Serializable]
    public class Interval {
        public int Id { get; set; }

        public IntervalState State { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }

        public string TimeAccountName { get; set; }

        public DateTime LastModified { get; set; }
    }
}
