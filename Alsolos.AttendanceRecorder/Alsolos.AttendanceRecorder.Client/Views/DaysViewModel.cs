namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Views.Model;
    using Alsolos.Commons.Mvvm;

    public class DaysViewModel : ViewModel
    {
        public DatePeriod DatePeriod
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, x => SetTitle()); }
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
            set { BackingFields.SetValue(value, x => Refresh()); }
        }

        private void SetTitle()
        {
            Title = DatePeriod != null ? DatePeriod.Name : null;
        }

        private void Refresh()
        {
            var firstDay = Days.FirstOrDefault();
            if (firstDay != null)
            {
                firstDay.IsExpanded = true;
            }

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
