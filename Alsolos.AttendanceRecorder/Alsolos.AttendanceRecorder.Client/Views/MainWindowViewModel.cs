﻿namespace Alsolos.AttendanceRecorder.Client.Views
{
    using Alsolos.Commons.Mvvm;

    public class MainWindowViewModel : ViewModel
    {
        public IntervalsSelectorViewModel IntervalsSelectorViewModel
        {
            get { return BackingFields.GetValue(() => new IntervalsSelectorViewModel()); }
        }
    }
}
