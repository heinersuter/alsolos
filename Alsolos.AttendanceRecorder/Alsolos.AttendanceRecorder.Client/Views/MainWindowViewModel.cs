using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class MainWindowViewModel : ViewModel {
        public IntervalsSelectorViewModel IntervalsSelectorViewModel {
            get { return BackingFields.GetValue(() => IntervalsSelectorViewModel, () => new IntervalsSelectorViewModel()); }
        }
    }
}
