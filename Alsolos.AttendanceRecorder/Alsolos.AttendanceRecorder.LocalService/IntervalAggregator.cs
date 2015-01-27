namespace Alsolos.AttendanceRecorder.LocalService
{
    using System.Collections.Generic;
    using System.Linq;
    using Alsolos.AttendanceRecorder.WebApi.Controllers;

    public class IntervalAggregator : IIntervalCollection
    {
        private readonly List<Interval> _intervals;
        private readonly TempStore _tempStore = new TempStore();

        public IntervalAggregator()
        {
            _intervals = _tempStore.Load();
        }

        public IEnumerable<IInterval> Intervals
        {
            get { return _intervals.Cast<IInterval>().ToList(); }
        }

        public void Add(Interval interval)
        {
            _intervals.Add(interval);
            _tempStore.Save(_intervals.Where(i => i.State != IntervalState.Persisted).ToList());
        }

        public void TrackUpdate(IInterval currentInterval)
        {
            _tempStore.Save(_intervals.Where(i => i.State != IntervalState.Persisted).ToList());
        }
    }
}
