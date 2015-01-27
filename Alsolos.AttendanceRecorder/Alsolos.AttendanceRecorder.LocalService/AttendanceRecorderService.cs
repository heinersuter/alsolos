namespace Alsolos.AttendanceRecorder.LocalService
{
    using System;
    using System.Linq;
    using Alsolos.AttendanceRecorder.WebApi;
    using Alsolos.AttendanceRecorder.WebApi.Controllers;

    public class AttendanceRecorderService : IDisposable
    {
        private readonly IntervalAggregator _aggregator;
        private readonly WebApiStarter _webApiStarter;

        public AttendanceRecorderService()
        {
            _aggregator = new IntervalAggregator();
            _webApiStarter = new WebApiStarter();
            _webApiStarter.Start();
        }

        public void KeepAlive(string timeAccountName, TimeSpan intervalDuration)
        {
            AddOrUpdateInterval(timeAccountName, intervalDuration, DateTime.Now);
        }

        public void Dispose()
        {
            if (_webApiStarter != null)
            {
                _webApiStarter.Dispose();
            }
        }

        private void AddOrUpdateInterval(string timeAccountName, TimeSpan intervalDuration, DateTime currentDateTime)
        {
            var currentInterval = FindIntervallToUpdate(timeAccountName, intervalDuration, currentDateTime);

            if (currentInterval != null)
            {
                UpdateExistingIntervall(currentInterval, currentDateTime);
                _aggregator.TrackUpdate(currentInterval);
            }
            else
            {
                _aggregator.Add(CreateNewIntervall(timeAccountName, currentDateTime));
            }
        }

        private IInterval FindIntervallToUpdate(string timeAccountName, TimeSpan intervalDuration, DateTime currentTime)
        {
            var currentInterval =
                _aggregator.Intervals.SingleOrDefault(
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

        private static void UpdateExistingIntervall(IInterval currentInterval, DateTime currentTime)
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
