namespace AttendanceRecorder.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using AttendanceRecorder.Model;

    public class EventTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventItem = value as EventItem;
            if (eventItem == null)
            {
                return null;
            }

            return string.Format("Changed from '{0}' to '{1}'.", eventItem.OldState, eventItem.NewState);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
