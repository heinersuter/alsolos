namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Services;
    using Alsolos.AttendanceRecorder.Client.Views.Model;
    using Alsolos.Commons.Mvvm;

    public class DaysViewModel : ViewModel
    {
        public DatePeriod DatePeriod
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, Load); }
        }

        public string Title
        {
            get { return BackingFields.GetValue<string>(); }
            private set { BackingFields.SetValue(value); }
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

        private async void Load(DatePeriod period)
        {
            if (period == null)
            {
                Title = "---";
                Days = new List<DayViewModel>();
            }
            else
            {
                Title = period.Name;

                var intervalService = new IntervalService();
                var intervals = await intervalService.GetIntervalsInRange(period.Start, period.End);

                var dayGroupings = intervals.GroupBy(interval => interval.Date.Date);
                Days = dayGroupings.Select(grouping => new DayViewModel(grouping.Key, grouping.ToList())).OrderByDescending(dayViewModel => dayViewModel.Date).ToList();
            }

            ExpandFirstDay();
            CalculateTotalTime();
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
