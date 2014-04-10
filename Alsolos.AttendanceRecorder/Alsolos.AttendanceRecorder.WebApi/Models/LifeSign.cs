using System;

namespace Alsolos.AttendanceRecorder.WebApi.Models {
    public class LifeSign {
        public string TimeAccountName { get; set; }

        public bool IsActive { get; set; }

        public TimeSpan IntervalDuration { get; set; }
    }
}