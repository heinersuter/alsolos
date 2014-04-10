using System;
using System.Collections.Generic;
using System.Linq;

namespace Alsolos.AttendanceRecorder.LocalService {
    public class AttendanceRecorderService {
        private readonly List<Interval> _intervals = new List<Interval>();

        public void KeepAlive(string timeAccountName, TimeSpan intervalDuration) {
            AddOrUpdateInterval(timeAccountName, intervalDuration);
        }

        private void AddOrUpdateInterval(string timeAccountName, TimeSpan intervalDuration) {
            var currentTime = DateTime.Now;

            var currentInterval = _intervals.SingleOrDefault(
                interval => interval.TimeAccountName == timeAccountName
                && interval.Date.Date == currentTime.Date
                && interval.Date + interval.End + intervalDuration + intervalDuration > currentTime);

            if (currentInterval != null) {
                currentInterval.State = IntervalState.Dirty;
                currentInterval.End = currentTime.TimeOfDay;
            } else {
                currentInterval = new Interval {
                    State = IntervalState.New,
                    Date = currentTime.Date,
                    Start = currentTime.TimeOfDay,
                    End = currentTime.TimeOfDay,
                    TimeAccountName = timeAccountName,
                };
                _intervals.Add(currentInterval);
            }

            // TODO: Save current interval on server
        }
    }
}
