namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System.Collections.Generic;
    using Alsolos.Commons.Mvvm;

    public class DaysViewModel : ViewModel
    {
        public IEnumerable<DayViewModel> Days
        {
            get { return BackingFields.GetValue<IEnumerable<DayViewModel>>(); }
            set { BackingFields.SetValue(value); }
        }
    }
}
