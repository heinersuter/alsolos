namespace AttendanceRecorder.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class CurrentDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var day = value as DateTime?;
            if (day == null)
            {
                return false;
            }

            return day.Value.Date == DateTime.Now.Date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
