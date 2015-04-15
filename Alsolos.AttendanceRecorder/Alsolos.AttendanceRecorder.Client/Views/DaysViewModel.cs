namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alsolos.Commons.Mvvm;

    public class DaysViewModel : ViewModel
    {
        public DatePeriod DatePeriod
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, x => Update()); }
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

        public IEnumerable<DayViewModel> Days
        {
            get { return BackingFields.GetValue<IEnumerable<DayViewModel>>(); }
            set { BackingFields.SetValue(value); }
        }

        private void Update()
        {
            SetTitle();
            SetTotalTime();
        }

        private void SetTitle()
        {
            Title = DatePeriod != null ? DatePeriod.Name : null;
        }

        private void SetTotalTime()
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
