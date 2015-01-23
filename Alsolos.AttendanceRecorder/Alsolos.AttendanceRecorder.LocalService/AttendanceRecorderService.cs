namespace Alsolos.AttendanceRecorder.LocalService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AttendanceRecorderService
    {
        private readonly List<Interval> _intervals;
        private readonly TempStore _tempStore = new TempStore();

        public AttendanceRecorderService()
        {
            _intervals = _tempStore.Load();
        }

        public void KeepAlive(string timeAccountName, TimeSpan intervalDuration)
        {
            Console.WriteLine("Keep Alive {0}", DateTime.Now);
            AddOrUpdateInterval(timeAccountName, intervalDuration);
        }

        private void AddOrUpdateInterval(string timeAccountName, TimeSpan intervalDuration)
        {
            var currentTime = DateTime.Now;

            var currentInterval = _intervals.SingleOrDefault(
                interval => interval.TimeAccountName == timeAccountName
                && interval.Date.Date == currentTime.Date
                && interval.Date + interval.End + intervalDuration + intervalDuration > currentTime);

            if (currentInterval != null)
            {
                currentInterval.State = IntervalState.Dirty;
                currentInterval.End = currentTime.TimeOfDay;
                currentInterval.LastModified = DateTime.Now;
            }
            else
            {
                currentInterval = new Interval
                {
                    State = IntervalState.New,
                    Date = currentTime.Date,
                    Start = currentTime.TimeOfDay,
                    End = currentTime.TimeOfDay,
                    TimeAccountName = timeAccountName,
                    LastModified = DateTime.Now,
                };
                _intervals.Add(currentInterval);
            }

            _tempStore.Save(_intervals.Where(interval => interval.State != IntervalState.Persisted).ToList());
        }
    }
}
