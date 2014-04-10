using System;
using System.Collections.Generic;
using Alsolos.AttendanceRecorder.Client.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class IntervalsSelectorViewModel : ViewModel {
        public IntervalsSelectorViewModel() {
            var intervals = new[] {
                new Interval { Date = DateTime.Today, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(9.5) },
                new Interval { Date = DateTime.Today, Start = TimeSpan.FromHours(9.75), End = TimeSpan.FromHours(12) },
            };

            DaysViewModel.Days = new List<DayViewModel> {
                new DayViewModel(intervals),
                new DayViewModel(intervals),
            };
        }

        public DaysViewModel DaysViewModel {
            get { return BackingFields.GetValue(() => DaysViewModel, () => new DaysViewModel()); }
        }
    }
}
