using System.Collections.Generic;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class DaysViewModel : ViewModel {
        public IEnumerable<DayViewModel> Days {
            get { return BackingFields.GetValue(() => Days); }
            set { BackingFields.SetValue(() => Days, value); }
        }
    }
}
