using System;
using System.Collections.Generic;
using Alsolos.AttendanceRecorder.Client.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class MainWindowViewModel : ViewModel {
        public MainWindowViewModel() {
            IEnumerable<Interval> modelIntervals = new[] {
                new Interval { Date = new DateTime(2014, 1, 1), Start = TimeSpan.FromHours(1), End = TimeSpan.FromHours(2) },
                new Interval { Date = new DateTime(2014, 1, 1), Start = TimeSpan.FromHours(3), End = TimeSpan.FromHours(4) },
                new Interval { Date = new DateTime(2014, 1, 6), Start = TimeSpan.FromHours(5), End = TimeSpan.FromHours(6) },
                new Interval { Date = new DateTime(2014, 1, 6), Start = TimeSpan.FromHours(7), End = TimeSpan.FromHours(8) },
                new Interval { Date = new DateTime(2013, 6, 6), Start = TimeSpan.FromHours(9), End = TimeSpan.FromHours(10) },
                new Interval { Date = new DateTime(2015, 6, 6), Start = TimeSpan.FromHours(11), End = TimeSpan.FromHours(12) },
                new Interval { Date = new DateTime(2014, 5, 6), Start = TimeSpan.FromHours(13), End = TimeSpan.FromHours(14) },
                new Interval { Date = new DateTime(2014, 5, 6), Start = TimeSpan.FromHours(15), End = TimeSpan.FromHours(16) },
                new Interval { Date = new DateTime(2014, 6, 6), Start = TimeSpan.FromHours(17), End = TimeSpan.FromHours(18) },
                new Interval { Date = new DateTime(2014, 6, 6), Start = TimeSpan.FromHours(19), End = TimeSpan.FromHours(20) },
                new Interval { Date = new DateTime(2014, 6, 7), Start = TimeSpan.FromHours(21), End = TimeSpan.FromHours(22) },
                new Interval { Date = new DateTime(2014, 6, 7), Start = TimeSpan.FromHours(23), End = TimeSpan.FromHours(24) },
            };
            IntervalsSelectorViewModel.SetIntervals(modelIntervals);
        }

        public IntervalsSelectorViewModel IntervalsSelectorViewModel {
            get { return BackingFields.GetValue(() => IntervalsSelectorViewModel, () => new IntervalsSelectorViewModel()); }
        }
    }
}
