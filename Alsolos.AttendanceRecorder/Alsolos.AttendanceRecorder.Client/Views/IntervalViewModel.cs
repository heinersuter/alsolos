namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using Alsolos.Commons.Mvvm;

    public class IntervalViewModel : BackingFieldsHolder
    {
        public DateTime Date
        {
            get { return BackingFields.GetValue<DateTime>(); }
            set { BackingFields.SetValue(value); }
        }

        public TimeSpan Start
        {
            get { return BackingFields.GetValue<TimeSpan>(); }
            set { BackingFields.SetValue(value, x => UpdateDuration()); }
        }

        public TimeSpan End
        {
            get { return BackingFields.GetValue<TimeSpan>(); }
            set { BackingFields.SetValue(value, x => UpdateDuration()); }
        }

        public IntervalType Type
        {
            get { return BackingFields.GetValue<IntervalType>(); }
            set { BackingFields.SetValue(value); }
        }

        public TimeSpan Duration
        {
            get { return BackingFields.GetValue<TimeSpan>(); }
            private set { BackingFields.SetValue(value); }
        }

        private TimeSpan UpdateDuration()
        {
            return Duration = End - Start;
        }
    }
}
