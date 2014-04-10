using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Alsolos.AttendanceRecorder.Client.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class DayViewModel : BackingFieldsHolder {
        public DayViewModel(IEnumerable<Interval> intervals) {
            Intervals = new ObservableCollection<IntervalViewModel>(CreateIntervalAdapters(intervals));
        }

        public ObservableCollection<IntervalViewModel> Intervals {
            get { return BackingFields.GetValue(() => Intervals); }
            private set { BackingFields.SetValue(() => Intervals, value); }
        }

        private static IEnumerable<IntervalViewModel> CreateIntervalAdapters(IEnumerable<Interval> intervals) {
            var intervalList = intervals as IList<Interval> ?? intervals.ToList();
            if (intervals == null || !intervalList.Any()) {
                return Enumerable.Empty<IntervalViewModel>();
            }
            var date = intervalList.First().Date.Date;
            var lastTime = TimeSpan.Zero;
            var result = new List<IntervalViewModel>();
            foreach (var interval in intervalList.Where(interval => interval.Date == date).OrderBy(interval => interval.Start)) {
                if (interval.Start > lastTime)
                {
                    result.Add(new IntervalViewModel { Date = date, Start = lastTime, End = interval.Start, Type = IntervalType.Inactive });
                }
                result.Add(new IntervalViewModel { Date = date, Start = interval.Start, End = interval.End, Type = IntervalType.Active });
                lastTime = interval.End;
            }
            return result;
        }
    }
}
