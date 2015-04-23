namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Services;
    using Alsolos.AttendanceRecorder.Client.Views.Model;
    using Alsolos.Commons.Mvvm;

    public class DaysViewModel : ViewModel
    {
        private readonly IntervalService _intervalService = new IntervalService();

        public DatePeriod DatePeriod
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, period => Load()); }
        }

        public TimeSpan TotalTime
        {
            get { return BackingFields.GetValue<TimeSpan>(); }
            private set { BackingFields.SetValue(value); }
        }

        public IList<DayViewModel> Days
        {
            get { return BackingFields.GetValue<IList<DayViewModel>>(); }
            private set { BackingFields.SetValue(value); }
        }

        private async void Load()
        {
            IsBusy = true;
            DetachPropertyChangedEventHandler();

            var intervals = await _intervalService.GetIntervalsInRangeAsync(DatePeriod.Start, DatePeriod.End);

            var dayGroupings = intervals.GroupBy(interval => interval.Date);
            Days = dayGroupings.Select(grouping =>
            {
                var dayViewModel = new DayViewModel(grouping.Key, grouping.ToList());
                dayViewModel.PropertyChanged += OnDayViewModelPropertyChanged;
                return dayViewModel;
            }).OrderByDescending(dayViewModel => dayViewModel.Date).ToList();

            ExpandFirstDay();
            CalculateTotalTime();
            IsBusy = false;
        }

        private void DetachPropertyChangedEventHandler()
        {
            if (Days != null)
            {
                foreach (var dayViewModel in Days)
                {
                    dayViewModel.PropertyChanged -= OnDayViewModelPropertyChanged;
                }
            }
        }

        private void OnDayViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == GetPropertyName(() => ((DayViewModel)sender).TotalTime))
            {
                CalculateTotalTime();
            }
        }

        private void ExpandFirstDay()
        {
            var firstDay = Days.FirstOrDefault();
            if (firstDay != null)
            {
                firstDay.IsExpanded = true;
            }
        }

        private void CalculateTotalTime()
        {
            if (Days != null)
            {
                TotalTime = Days.Aggregate(TimeSpan.Zero, (total, currentViewModel) => total + currentViewModel.TotalTime);
            }
            else
            {
                TotalTime = TimeSpan.Zero;
            }
        }
    }
}
