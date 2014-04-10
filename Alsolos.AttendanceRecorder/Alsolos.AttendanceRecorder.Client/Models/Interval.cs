using System;

namespace Alsolos.AttendanceRecorder.Client.Models {
    public class Interval {
        public DateTime Date { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}
