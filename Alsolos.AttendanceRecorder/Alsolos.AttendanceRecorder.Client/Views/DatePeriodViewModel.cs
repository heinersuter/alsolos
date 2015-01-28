namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Alsolos.AttendanceRecorder.Client.Models;
    using Alsolos.Commons.Mvvm;

    public class DatePeriodViewModel : ViewModel
    {
        public IEnumerable<DatePeriod> Years
        {
            get { return BackingFields.GetValue(() => Years); }
            private set { BackingFields.SetValue(() => Years, value); }
        }

        public IEnumerable<DatePeriod> Months
        {
            get { return BackingFields.GetValue(() => Months); }
            private set { BackingFields.SetValue(() => Months, value); }
        }

        public IEnumerable<DatePeriod> Weeks
        {
            get { return BackingFields.GetValue(() => Weeks); }
            private set { BackingFields.SetValue(() => Weeks, value); }
        }

        public int SelectedTabIndex
        {
            get { return BackingFields.GetValue(() => SelectedTabIndex); }
            set { BackingFields.SetValue(() => SelectedTabIndex, value, SelectedTabIndexChanged); }
        }

        public int SelectedWeekIndex
        {
            get { return BackingFields.GetValue(() => SelectedWeekIndex); }
            set { BackingFields.SetValue(() => SelectedWeekIndex, value); }
        }

        public int SelectedMonthIndex
        {
            get { return BackingFields.GetValue(() => SelectedMonthIndex); }
            set { BackingFields.SetValue(() => SelectedMonthIndex, value); }
        }

        public int SelectedYearIndex
        {
            get { return BackingFields.GetValue(() => SelectedYearIndex); }
            set { BackingFields.SetValue(() => SelectedYearIndex, value); }
        }

        public DatePeriod SelectedYear
        {
            get { return BackingFields.GetValue(() => SelectedYear); }
            set { BackingFields.SetValue(() => SelectedYear, value, UpdateSelection); }
        }

        public DatePeriod SelectedMonth
        {
            get { return BackingFields.GetValue(() => SelectedMonth); }
            set { BackingFields.SetValue(() => SelectedMonth, value, UpdateSelection); }
        }

        public DatePeriod SelectedWeek
        {
            get { return BackingFields.GetValue(() => SelectedWeek); }
            set { BackingFields.SetValue(() => SelectedWeek, value, UpdateSelection); }
        }

        public DatePeriod SelectedPeriod
        {
            get { return BackingFields.GetValue(() => SelectedPeriod); }
            private set { BackingFields.SetValue(() => SelectedPeriod, value); }
        }

        public void SetIntervals(IEnumerable<Interval> modelIntervals)
        {
            var intervalList = modelIntervals as IList<Interval> ?? modelIntervals.ToList();
            InitYears(intervalList);
            InitMonths(intervalList);
            InitWeeks(intervalList);
            SelectedTabIndex = 1;
        }

        private void SelectedTabIndexChanged(int index)
        {
            switch (index)
            {
                case 0:
                    if (Years != null)
                    {
                        SelectedYear = Years.FirstOrDefault();
                        SelectedYearIndex = 0;
                    }
                    break;
                case 2:
                    if (Weeks != null)
                    {
                        SelectedWeek = Weeks.FirstOrDefault();
                        SelectedWeekIndex = 0;
                    }
                    break;
                default:
                    if (Months != null)
                    {
                        SelectedMonth = Months.FirstOrDefault();
                        SelectedMonthIndex = 0;
                    }
                    break;
            }
        }

        private void UpdateSelection(DatePeriod selectedPeriod)
        {
            SelectedPeriod = selectedPeriod;
        }

        private void InitYears(IEnumerable<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => interval.Date.Year)
                .OrderBy(grouping => grouping.Key);
            Years = groupings.Select(grouping => new DatePeriod(
                grouping.Key.ToString(CultureInfo.InvariantCulture),
                new DateTime(grouping.Key, 1, 1),
                new DateTime(grouping.Key, 12, 31))).OrderByDescending(period => period.Start);
        }

        private void InitMonths(IEnumerable<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => new YearMonth(interval.Date))
                .OrderBy(grouping => grouping.Key.Year)
                .ThenBy(grouping => grouping.Key.Month);
            Months = groupings.Select(grouping => grouping.Key.ToDatePeriod()).OrderByDescending(period => period.Start);
        }

        private void InitWeeks(IEnumerable<Interval> modelIntervals)
        {
            var groupings = modelIntervals.GroupBy(interval => new YearWeek(interval.Date))
                .OrderBy(grouping => grouping.Key.Year)
                .ThenBy(grouping => grouping.Key.Week);
            Weeks = groupings.Select(grouping => grouping.Key.ToDatePeriod()).OrderByDescending(period => period.Start);
        }
    }
}
