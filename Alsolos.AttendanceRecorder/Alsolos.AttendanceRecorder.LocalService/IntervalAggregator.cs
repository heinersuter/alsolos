namespace Alsolos.AttendanceRecorder.LocalService
{
    using System.Collections.Generic;
    using System.Linq;
    using Alsolos.AttendanceRecorder.WebApi.Controllers;

    public class IntervalAggregator : IIntervalCollection
    {
        private readonly IList<Interval> _intervals;
        private readonly LocalFileSystemStore _localFileSystemStore = new LocalFileSystemStore();

        public IntervalAggregator()
        {
            _intervals = _localFileSystemStore.Load();
        }

        public IEnumerable<IInterval> Intervals
        {
            get { return _intervals.Cast<IInterval>().ToList(); }
        }

        public void Add(Interval interval)
        {
            _intervals.Add(interval);
            SaveUnpersistedValues();
        }

        private void SaveUnpersistedValues()
        {
            _localFileSystemStore.Save(_intervals.Where(i => i.State != IntervalState.Persisted).ToList());
        }

        public bool Remove(IInterval interval)
        {
            var intervalToRemove = _intervals.SingleOrDefault(inner => inner.Date == interval.Date && inner.Start == interval.Start);
            if (intervalToRemove != null)
            {
                var result = _intervals.Remove(intervalToRemove);
                if (result)
                {
                    SaveUnpersistedValues();
                }
                return result;
            }
            return false;
        }

        public void SaveIntervals()
        {
            SaveUnpersistedValues();
        }
    }
}
