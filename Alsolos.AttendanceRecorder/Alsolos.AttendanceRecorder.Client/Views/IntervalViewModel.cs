namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using Alsolos.Commons.Mvvm;

    public class IntervalViewModel : BackingFieldsHolder
    {
        public DateTime Date
        {
            get { return BackingFields.GetValue(() => Date); }
            set { BackingFields.SetValue(() => Date, value); }
        }

        public TimeSpan Start
        {
            get { return BackingFields.GetValue(() => Start); }
            set { BackingFields.SetValue(() => Start, value); }
        }

        public TimeSpan End
        {
            get { return BackingFields.GetValue(() => End); }
            set { BackingFields.SetValue(() => End, value, span => Duration = End - Start); }
        }

        public IntervalType Type
        {
            get { return BackingFields.GetValue(() => Type); }
            set { BackingFields.SetValue(() => Type, value); }
        }

        public TimeSpan Duration
        {
            get { return BackingFields.GetValue(() => Duration); }
            private set { BackingFields.SetValue(() => Duration, value); }
        }
    }
}
