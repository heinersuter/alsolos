namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Models;
    using Alsolos.Commons.Mvvm;

    public class DayViewModel : BackingFieldsHolder
    {
        private readonly TimeSpan _midnight = new TimeSpan(23, 59, 59);

        public DayViewModel(DateTime date, IEnumerable<Interval> modelIntervals)
        {
            Date = date.Date;
            Init(modelIntervals.Where(interval => interval.Date.Date == Date).OrderBy(interval => interval.Start));
        }

        public DateTime Date
        {
            get { return BackingFields.GetValue<DateTime>(); }
            private set { BackingFields.SetValue(value); }
        }

        public ObservableCollection<IntervalViewModel> Intervals
        {
            get { return BackingFields.GetValue<ObservableCollection<IntervalViewModel>>(); }
            private set { BackingFields.SetValue(value); }
        }

        public TimeSpan TotalTime
        {
            get
            {
                return Intervals
                    .Where(viewModel => viewModel.Type == IntervalType.Active)
                    .Aggregate(TimeSpan.Zero, (total, currentViewModel) => total + currentViewModel.Duration);
            }
        }

        private void Init(IEnumerable<Interval> modelIntervals)
        {
            var intervalList = modelIntervals as IList<Interval> ?? modelIntervals.ToList();
            if (modelIntervals == null || !intervalList.Any())
            {
                Intervals = new ObservableCollection<IntervalViewModel>();
                return;
            }

            var lastTime = TimeSpan.Zero;
            var intervals = new List<IntervalViewModel>();
            foreach (var interval in intervalList)
            {
                if (interval.Start > lastTime)
                {
                    // Add inactive intervall
                    intervals.Add(new IntervalViewModel { Date = Date, Start = lastTime, End = interval.Start, Type = IntervalType.Inactive });
                }

                // Add active intervall
                intervals.Add(new IntervalViewModel { Date = Date, Start = interval.Start, End = interval.End, Type = IntervalType.Active });
                lastTime = interval.End;
            }
            if (lastTime < _midnight)
            {
                // Add last inactive interval to midnight
                intervals.Add(new IntervalViewModel { Date = Date, Start = lastTime, End = _midnight, Type = IntervalType.Inactive });
            }
            Intervals = new ObservableCollection<IntervalViewModel>(intervals);
        }
    }
}
