using System;
using System.Globalization;

namespace Alsolos.Commons.Mvvm.Converters {
    public class BoolToValueConverter : ValueConverter {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isTrue = (value as bool?) == true;

            if (parameter != null) {
                isTrue = !isTrue;
            }

            return isTrue ? TrueValue : FalseValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
