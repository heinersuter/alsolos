using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Alsolos.AttendanceRecorder.Client.Models;
using Alsolos.Commons.Mvvm;

namespace Alsolos.AttendanceRecorder.Client.Views {
    public class DatePeriodViewModel : ViewModel {
        public IEnumerable<DatePeriod> Years {
            get { return BackingFields.GetValue(() => Years); }
            private set { BackingFields.SetValue(() => Years, value); }
        }

        public IEnumerable<DatePeriod> Months {
            get { return BackingFields.GetValue(() => Months); }
            private set { BackingFields.SetValue(() => Months, value); }
        }

        public IEnumerable<DatePeriod> Weeks {
            get { return BackingFields.GetValue(() => Weeks); }
            private set { BackingFields.SetValue(() => Weeks, value); }
        }

        public DatePeriod SelectedYear {
            get { return BackingFields.GetValue(() => SelectedYear); }
            set { BackingFields.SetValue(() => SelectedYear, value, UpdateSelection); }
        }

        public DatePeriod SelectedMonth {
            get { return BackingFields.GetValue(() => SelectedMonth); }
            set { BackingFields.SetValue(() => SelectedMonth, value, UpdateSelection); }
        }

        public DatePeriod SelectedWeek {
            get { return BackingFields.GetValue(() => SelectedWeek); }
            set { BackingFields.SetValue(() => SelectedWeek, value, UpdateSelection); }
        }

        public DatePeriod SelectedPeriod {
            get { return BackingFields.GetValue(() => SelectedPeriod); }
            private set { BackingFields.SetValue(() => SelectedPeriod, value); }
        }

        public void SetIntervals(IEnumerable<Interval> modelIntervals) {
            var intervalList = modelIntervals as IList<Interval> ?? modelIntervals.ToList();
            InitYears(intervalList);
            InitMonths(intervalList);
            InitWeeks(intervalList);
            if (Months != null) {
                SelectedMonth = Months.FirstOrDefault();
            }
        }

        private void UpdateSelection(DatePeriod selectedPeriod) {
            if (selectedPeriod == null) {
                return;
            }
            SelectedPeriod = selectedPeriod;
            if (SelectedYear != selectedPeriod) {
                SelectedYear = null;
            }
            if (SelectedMonth != selectedPeriod) {
                SelectedMonth = null;
            }
            if (SelectedWeek != selectedPeriod) {
                SelectedWeek = null;
            }
        }

        private void InitYears(IEnumerable<Interval> modelIntervals) {
            var groupings = modelIntervals.GroupBy(interval => interval.Date.Year)
                .OrderBy(grouping => grouping.Key);
            Years = groupings.Select(grouping => new DatePeriod(
                grouping.Key.ToString(CultureInfo.InvariantCulture),
                new DateTime(grouping.Key, 1, 1),
                new DateTime(grouping.Key, 12, 31)));
        }

        private void InitMonths(IEnumerable<Interval> modelIntervals) {
            var groupings = modelIntervals.GroupBy(interval => new YearMonth(interval.Date))
                .OrderBy(grouping => grouping.Key.Year)
                .ThenBy(grouping => grouping.Key.Month);
            Months = groupings.Select(grouping => grouping.Key.ToDatePeriod());
        }

        private void InitWeeks(IEnumerable<Interval> modelIntervals) {
            var groupings = modelIntervals.GroupBy(interval => new YearWeek(interval.Date))
                .OrderBy(grouping => grouping.Key.Year)
                .ThenBy(grouping => grouping.Key.Week);
            Weeks = groupings.Select(grouping => grouping.Key.ToDatePeriod());
        }
    }
}
