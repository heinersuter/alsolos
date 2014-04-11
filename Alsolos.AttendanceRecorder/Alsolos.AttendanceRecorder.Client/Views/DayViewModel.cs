using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Alsolos.AttendanceRecorder.Client.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class DayViewModel : BackingFieldsHolder {
        public DayViewModel(DateTime date, IEnumerable<Interval> modelIntervals) {
            Date = date.Date;
            Init(modelIntervals);
        }

        public DateTime Date {
            get { return BackingFields.GetValue(() => Date); }
            private set { BackingFields.SetValue(() => Date, value); }
        }

        public ObservableCollection<IntervalViewModel> Intervals {
            get { return BackingFields.GetValue(() => Intervals); }
            private set { BackingFields.SetValue(() => Intervals, value); }
        }

        private void Init(IEnumerable<Interval> modelIntervals) {
            var intervalList = modelIntervals as IList<Interval> ?? modelIntervals.ToList();
            if (modelIntervals == null || !intervalList.Any()) {
                Intervals = new ObservableCollection<IntervalViewModel>();
                return;
            }

            var lastTime = TimeSpan.Zero;
            var intervals = new List<IntervalViewModel>();
            foreach (var interval in intervalList.Where(interval => interval.Date == Date).OrderBy(interval => interval.Start)) {
                if (interval.Start > lastTime) {
                    intervals.Add(new IntervalViewModel { Date = Date, Start = lastTime, End = interval.Start, Type = IntervalType.Inactive });
                }
                intervals.Add(new IntervalViewModel { Date = Date, Start = interval.Start, End = interval.End, Type = IntervalType.Active });
                lastTime = interval.End;
            }
            Intervals = new ObservableCollection<IntervalViewModel>(intervals);
        }
    }
}
