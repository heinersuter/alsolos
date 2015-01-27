namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Models;
    using Alsolos.Commons.Mvvm;

    public class IntervalsSelectorViewModel : ViewModel
    {
        private IEnumerable<Interval> _modelIntervals;

        public IntervalsSelectorViewModel()
        {
            DatePeriodViewModel.PropertyChanged += OnDatePeriodViewModelPropertyChanged;
        }

        public DatePeriodViewModel DatePeriodViewModel
        {
            get { return BackingFields.GetValue(() => DatePeriodViewModel, () => new DatePeriodViewModel()); }
        }

        public DaysViewModel DaysViewModel
        {
            get { return BackingFields.GetValue(() => DaysViewModel, () => new DaysViewModel()); }
        }

        public void SetIntervals(IEnumerable<Interval> modelIntervals)
        {
            _modelIntervals = modelIntervals as IList<Interval> ?? modelIntervals.ToList();
            DatePeriodViewModel.SetIntervals(_modelIntervals);
            SetDays();
        }

        private void OnDatePeriodViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GetPropertyName(() => DatePeriodViewModel.SelectedPeriod))
            {
                SetDays();
            }
        }

        private void SetDays()
        {
            if (DatePeriodViewModel.SelectedPeriod == null)
            {
                DaysViewModel.Days = Enumerable.Empty<DayViewModel>();
                return;
            }
            var selectedIntervals = _modelIntervals.Where(interval => interval.Date >= DatePeriodViewModel.SelectedPeriod.Start && interval.Date <= DatePeriodViewModel.SelectedPeriod.End);
            var dayGroupings = selectedIntervals.GroupBy(interval => interval.Date.Date);
            DaysViewModel.Days = dayGroupings.Select(grouping => new DayViewModel(grouping.Key, grouping));
        }
    }
}
