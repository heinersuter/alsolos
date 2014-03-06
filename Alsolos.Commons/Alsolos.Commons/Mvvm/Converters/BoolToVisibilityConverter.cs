namespace Alsolos.Commons.Mvvm.Converters {
    using System;
    using System.Globalization;
    using System.Windows;

    public class BoolToVisibilityConverter : ValueConverter {
        public BoolToVisibilityConverter() {
            TrueVisibility = Visibility.Visible;
            FalseVisibility = Visibility.Collapsed;
        }

        public Visibility TrueVisibility { get; set; }
        public Visibility FalseVisibility { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isTrue = (value as bool?) == true;

            if (parameter != null) {
                isTrue = !isTrue;
            }

            return isTrue ? TrueVisibility : FalseVisibility;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
