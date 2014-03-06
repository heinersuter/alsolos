using System;
using System.Globalization;

namespace Alsolos.Commons.Mvvm.Converters {
    public class EqualityToValueConverter : ValueConverter {
        public object EqualValue { get; set; }
        public object NotEqualValue { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var areEqual = value == parameter;

            return areEqual ? EqualValue : NotEqualValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
