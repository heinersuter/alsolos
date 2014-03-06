namespace Alsolos.Commons.Mvvm.Converters {
    using System;
    using System.Globalization;

    public class BoolToInverseBoolConverter : ValueConverter {
        public static readonly BoolToInverseBoolConverter Instance = new BoolToInverseBoolConverter();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isTrue = (value as bool?) == true;
            return !isTrue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            var isTrue = (value as bool?) == true;
            return !isTrue;
        }
    }
}