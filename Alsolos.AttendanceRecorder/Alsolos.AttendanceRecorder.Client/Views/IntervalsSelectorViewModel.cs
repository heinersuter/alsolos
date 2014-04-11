using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Alsolos.AttendanceRecorder.Client.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class IntervalsSelectorViewModel : ViewModel {
        private readonly IEnumerable<Interval> _modelIntervals;

        public IntervalsSelectorViewModel() {
            _modelIntervals = new[] {
                new Interval { Date = DateTime.Today, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(9.5) },
                new Interval { Date = DateTime.Today, Start = TimeSpan.FromHours(9.75), End = TimeSpan.FromHours(12) },
                new Interval { Date = DateTime.Today.AddDays(-1), Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(9.5) },
                new Interval { Date = DateTime.Today.AddDays(-1), Start = TimeSpan.FromHours(9.75), End = TimeSpan.FromHours(12) },
            };

            DatePeriodViewModel = new DatePeriodViewModel(_modelIntervals);
            DatePeriodViewModel.PropertyChanged += OnDatePeriodViewModelPropertyChanged;
        }

        public DatePeriodViewModel DatePeriodViewModel {
            get { return BackingFields.GetValue(() => DatePeriodViewModel); }
            private set { BackingFields.SetValue(() => DatePeriodViewModel, value); }
        }

        public DaysViewModel DaysViewModel {
            get { return BackingFields.GetValue(() => DaysViewModel, () => new DaysViewModel()); }
        }

        private void OnDatePeriodViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == GetPropertyName(() => DatePeriodViewModel.SelectedPeriod)) {
                SetDays();
            }
        }

        private void SetDays() {
            var selectedIntervals = _modelIntervals.Where(interval => interval.Date >= DatePeriodViewModel.SelectedPeriod.Start && interval.Date <= DatePeriodViewModel.SelectedPeriod.End);
            var dayGroupings = selectedIntervals.GroupBy(interval => interval.Date.Date);
            DaysViewModel.Days = dayGroupings.Select(grouping => new DayViewModel(grouping.Key, grouping));
        }
    }
}
