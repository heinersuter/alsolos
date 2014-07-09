namespace AttendanceRecorder.View
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using AttendanceRecorder.Model;

    public class EventToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as EventItem;
            if (item == null)
            {
                return null;
            }

            if (item.NewState == SystemState.Active)
            {
                if (item.OldState == SystemState.Down)
                {
                    return "images/UpGreen16.png";
                }

                if (item.OldState == SystemState.Locked)
                {
                    return "images/UpBlue16.png";
                }
            }
            else
            {
                if (item.NewState == SystemState.Down)
                {
                    return "images/DownOrange16.png";
                }

                if (item.NewState == SystemState.Locked)
                {
                    return "images/DownGray16.png";
                }

                if (item.NewState == SystemState.Now)
                {
                    return "images/MinusGreen16.png";
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
