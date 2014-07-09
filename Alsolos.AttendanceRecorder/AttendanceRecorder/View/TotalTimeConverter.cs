namespace AttendanceRecorder.View
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Data;
    using AttendanceRecorder.Model;

    /// <summary>
    /// Use a MultiValueConverter so the value is evaluated after the PropertyChanged event of each Binding.
    /// </summary>
    public class TotalTimeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                return TimeSpan.Zero;
            }

            var events = values[0] as IEnumerable<EventItem>;
            if (events == null)
            {
                return TimeSpan.Zero;
            }

            return CalculateTime(events).ToString(@"hh\:mm\:ss");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static TimeSpan CalculateTime(IEnumerable<EventItem> events)
        {
            var totalTime = TimeSpan.Zero;

            TimeSpan? startTime = null;
            foreach (var eventItem in events)
            {
                if (eventItem.NewState == SystemState.Active)
                {
                    startTime = eventItem.Time.TimeOfDay;
                }
                else
                {
                    if (startTime != null)
                    {
                        totalTime += eventItem.Time.TimeOfDay - startTime.Value;
                    }

                    startTime = null;
                }
            }

            return totalTime;
        }
   }
}
