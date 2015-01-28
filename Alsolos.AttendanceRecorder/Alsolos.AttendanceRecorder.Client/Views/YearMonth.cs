namespace Alsolos.AttendanceRecorder.Client.Views
{
    using System;
    using System.Globalization;

    public struct YearMonth
    {
        private readonly int _year;
        private readonly int _month;

        public YearMonth(int year, int month)
        {
            _year = year;
            _month = month;
        }

        public YearMonth(DateTime date)
        {
            _year = date.Year;
            _month = date.Month;
        }

        public int Year
        {
            get { return _year; }
        }

        public int Month
        {
            get { return _month; }
        }

        public DatePeriod ToDatePeriod()
        {
            return new DatePeriod(
                string.Format(CultureInfo.CurrentCulture, "{0:D4} - {1}", Year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month)),
                new DateTime(Year, Month, 1),
                new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month)));
        }
    }
}