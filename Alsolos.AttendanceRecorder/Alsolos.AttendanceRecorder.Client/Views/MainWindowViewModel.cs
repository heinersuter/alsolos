namespace Alsolos.AttendanceRecorder.Client.Views
{
    using Alsolos.AttendanceRecorder.Client.Services;
    using Alsolos.Commons.Mvvm;

    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            LoadIntervals();
        }

        public IntervalsSelectorViewModel IntervalsSelectorViewModel
        {
            get { return BackingFields.GetValue(() => IntervalsSelectorViewModel, () => new IntervalsSelectorViewModel()); }
        }

        private async void LoadIntervals()
        {
            var intervalService = new IntervalService();
            var modelIntervals = await intervalService.GetIntervals();
            IntervalsSelectorViewModel.SetIntervals(modelIntervals);
        }
    }
}
