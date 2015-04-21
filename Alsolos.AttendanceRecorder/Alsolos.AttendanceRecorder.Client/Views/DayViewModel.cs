namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Models;
    using Alsolos.AttendanceRecorder.Client.Services;
    using Alsolos.AttendanceRecorder.Client.Views.Model;
    using Alsolos.AttendanceRecorder.WebApiModel;
    using Alsolos.Commons.Mvvm;

    public class DayViewModel : BackingFieldsHolder
    {
        private readonly TimeSpan _midnight = new TimeSpan(23, 59, 59);
        private readonly IntervalService _intervalService = new IntervalService();

        public DayViewModel(Date date, IList<Interval> modelIntervals)
        {
            Date = date;
            Init(modelIntervals.Where(interval => interval.Date == Date).OrderBy(interval => interval.Start).ToList());
        }

        public Date Date
        {
            get { return BackingFields.GetValue<Date>(); }
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

        public bool IsExpanded
        {
            get { return BackingFields.GetValue<bool>(); }
            set { BackingFields.SetValue(value); }
        }

        public DelegateCommand<IntervalViewModel> DeleteCommand
        {
            get { return BackingFields.GetCommand<IntervalViewModel>(Delete); }
        }

        private async void Delete(IntervalViewModel interval)
        {
            if (interval.Type == IntervalType.Active)
            {
                await _intervalService.RemoveInterval(interval.AsInterval());
                var intervals = await _intervalService.GetIntervalsInRange(Date, Date);
                Init(intervals.ToList());
            }
            else if (interval.Type == IntervalType.Inactive)
            {
                var currentIndex = Intervals.IndexOf(interval);
                var previous = Intervals[currentIndex - 1];
                var next = Intervals[currentIndex + 1];
                await _intervalService.MergeIntervals(previous.AsInterval(), next.AsInterval());
                var intervals = await _intervalService.GetIntervalsInRange(Date, Date);
                Init(intervals.ToList());
            }
        }

        private void Init(IList<Interval> modelIntervals)
        {
            if (modelIntervals == null || !modelIntervals.Any())
            {
                Intervals = new ObservableCollection<IntervalViewModel>();
                return;
            }

            var lastTime = TimeSpan.Zero;
            var intervals = new List<IntervalViewModel>();
            foreach (var interval in modelIntervals)
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
