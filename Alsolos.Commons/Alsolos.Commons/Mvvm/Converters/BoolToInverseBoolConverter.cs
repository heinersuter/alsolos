namespace Alsolos.Commons.Mvvm.Converters {
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class BoolToInverseBoolConverter : IValueConverter {
        public static readonly BoolToInverseBoolConverter Instance = new BoolToInverseBoolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return !(bool)value;
        }
    }
}