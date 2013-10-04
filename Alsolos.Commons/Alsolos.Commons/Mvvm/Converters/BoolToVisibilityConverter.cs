namespace Alsolos.Commons.Mvvm.Converters {
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class BoolToVisibilityConverter : IValueConverter {
        public static readonly BoolToVisibilityConverter Instance = new BoolToVisibilityConverter();

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var boolValue = false;

            if (value is bool) {
                boolValue = (bool)value;
            }

            if (parameter != null) {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}