using System;

namespace Alsolos.AttendanceRecorder.WebApi.Models {
    public class Interval {
        public bool IsPersited { get; set; }

        public string TimeAccountName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}