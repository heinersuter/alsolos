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

        public int SelectedYearIndex
        {
            get { return BackingFields.GetValue(() => -1); }
            set { BackingFields.SetValue(value, x => UpdateSelection(DatePeriodType.Year)); }
        }

        public int SelectedMonthIndex
        {
            get { return BackingFields.GetValue(() => -1); }
            set { BackingFields.SetValue(value, x => UpdateSelection(DatePeriodType.Month)); }
        }

        public int SelectedWeekIndex
        {
            get { return BackingFields.GetValue(() => -1); }
            set { BackingFields.SetValue(value, x => UpdateSelection(DatePeriodType.Week)); }
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
            SelectedWeekIndex = Weeks.Count > 0 ? 0 : -1;
        }

        private void UpdateSelection(DatePeriodType type)
        {
            switch (type)
            {
                case DatePeriodType.Year:
                    if (SelectedYearIndex >= 0)
                    {
                        SelectedMonthIndex = -1;
                        SelectedWeekIndex = -1;
                        SelectedPeriod = Years[SelectedYearIndex];
                    }
                    break;
                case DatePeriodType.Month:
                    if (SelectedMonthIndex >= 0)
                    {
                        SelectedYearIndex = -1;
                        SelectedWeekIndex = -1;
                        SelectedPeriod = Months[SelectedMonthIndex];
                    }
                    break;
                case DatePeriodType.Week:
                    if (SelectedWeekIndex >= 0)
                    {
                        SelectedYearIndex = -1;
                        SelectedMonthIndex = -1;
                        SelectedPeriod = Weeks[SelectedWeekIndex];
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        private void InitYears(IList<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => interval.Date.Year)
                .OrderBy(grouping => grouping.Key);
            Years = groupings.Select(grouping => new DatePeriod(
                DatePeriodType.Year,
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
