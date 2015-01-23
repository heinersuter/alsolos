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
            AddOrUpdateInterval(timeAccountName, intervalDuration, DateTime.Now);
        }

        private void AddOrUpdateInterval(string timeAccountName, TimeSpan intervalDuration, DateTime currentDateTime)
        {
            var currentInterval = FindIntervallToUpdate(timeAccountName, intervalDuration, currentDateTime);

            if (currentInterval != null)
            {
                UpdateExistingIntervall(currentInterval, currentDateTime);
            }
            else
            {
                _intervals.Add(CreateNewIntervall(timeAccountName, currentDateTime));
            }

            _tempStore.Save(_intervals.Where(interval => interval.State != IntervalState.Persisted).ToList());
        }

        private Interval FindIntervallToUpdate(string timeAccountName, TimeSpan intervalDuration, DateTime currentTime)
        {
            var currentInterval =
                _intervals.SingleOrDefault(
                    interval => interval.TimeAccountName == timeAccountName
                        && interval.Date.Date == currentTime.Date
                        && interval.Date + interval.End + intervalDuration + intervalDuration > currentTime);
            return currentInterval;
        }

        private static Interval CreateNewIntervall(string timeAccountName, DateTime currentTime)
        {
            var currentInterval = new Interval
            {
                State = IntervalState.New,
                Date = currentTime.Date,
                Start = GetTimeOfDay(currentTime),
                End = GetTimeOfDay(currentTime),
                TimeAccountName = timeAccountName,
                LastModified = DateTime.Now,
            };
            Console.WriteLine("Interval created: {0}", currentInterval.End);
            return currentInterval;
        }

        private static void UpdateExistingIntervall(Interval currentInterval, DateTime currentTime)
        {
            currentInterval.State = IntervalState.Dirty;
            currentInterval.End = GetTimeOfDay(currentTime);
            currentInterval.LastModified = DateTime.Now;
            Console.WriteLine("Interval updated: {0}", currentInterval.End);
        }

        private static TimeSpan GetTimeOfDay(DateTime currentTime)
        {
            return new TimeSpan(currentTime.Hour, currentTime.Minute, currentTime.Second);
        }
    }
}
