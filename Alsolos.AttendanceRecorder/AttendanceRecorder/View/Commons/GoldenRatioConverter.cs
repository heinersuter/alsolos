namespace AttendanceRecorder.View.Commons
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class GoldenRatioConverter : IValueConverter
    {
        private static readonly double _goldenFactor = (1.0 + Math.Sqrt(5.0)) / 2.0;
        private static readonly GridLength _goldenFactorGridLength = new GridLength(_goldenFactor, GridUnitType.Star);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return CalculateLongSide((double)value);
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static double GoldenFactor
        {
            get { return _goldenFactor; }
        }

        public static GridLength GoldenFactorGridLength
        {
            get { return _goldenFactorGridLength; }
        }

        public static double CalculateLongSide(double shortSide)
        {
            return shortSide * GoldenFactor;
        }

        public static double CalculateShortSide(double longSide)
        {
            return longSide / GoldenFactor;
        }
    }
}
