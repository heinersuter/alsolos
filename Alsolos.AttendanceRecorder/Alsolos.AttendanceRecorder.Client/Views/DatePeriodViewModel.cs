namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Models;
    using Alsolos.AttendanceRecorder.Client.Views.Model;
    using Alsolos.Commons.Mvvm;

    public class DatePeriodViewModel : ViewModel
    {
        public IList<DatePeriod> Years
        {
            get { return BackingFields.GetValue<IList<DatePeriod>>(); }
            private set { BackingFields.SetValue(value); }
        }

        public IList<DatePeriod> Months
        {
            get { return BackingFields.GetValue<IList<DatePeriod>>(); }
            private set { BackingFields.SetValue(value); }
        }

        public IList<DatePeriod> Weeks
        {
            get { return BackingFields.GetValue<IList<DatePeriod>>(); }
            private set { BackingFields.SetValue(value); }
        }

        public DatePeriod SelectedYear
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, UpdateSelection); }
        }

        public DatePeriod SelectedMonth
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, UpdateSelection); }
        }

        public DatePeriod SelectedWeek
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            set { BackingFields.SetValue(value, UpdateSelection); }
        }

        public DatePeriod SelectedPeriod
        {
            get { return BackingFields.GetValue<DatePeriod>(); }
            private set { BackingFields.SetValue(value); }
        }

        public void SetIntervals(IList<Interval> modelIntervals)
        {
            InitYears(modelIntervals);
            InitMonths(modelIntervals);
            InitWeeks(modelIntervals);
            SelectedWeek = Weeks.FirstOrDefault();
        }

        private void UpdateSelection(DatePeriod selectedPeriod)
        {
            SelectedPeriod = selectedPeriod;
        }

        private void InitYears(IList<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => interval.Date.Year)
                .OrderBy(grouping => grouping.Key);
            Years = groupings.Select(grouping => new DatePeriod(
                grouping.Key.ToString(CultureInfo.InvariantCulture),
                new DateTime(grouping.Key, 1, 1),
                new DateTime(grouping.Key, 12, 31))).OrderByDescending(period => period.Start).ToList();
        }

        private void InitMonths(IList<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => new YearMonth(interval.Date))
                .OrderBy(grouping => grouping.Key.Year)
                .ThenBy(grouping => grouping.Key.Month);
            Months = groupings.Select(grouping => grouping.Key.ToDatePeriod()).OrderByDescending(period => period.Start).ToList();
        }

        private void InitWeeks(IList<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => new YearWeek(interval.Date))
                .OrderBy(grouping => grouping.Key.Year)
                .ThenBy(grouping => grouping.Key.Week);
            Weeks = groupings.Select(grouping => grouping.Key.ToDatePeriod()).OrderByDescending(period => period.Start).ToList();
        }
    }
}
