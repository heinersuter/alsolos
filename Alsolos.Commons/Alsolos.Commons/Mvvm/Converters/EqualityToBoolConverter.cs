using System;
using System.Globalization;

namespace Alsolos.Commons.Mvvm.Converters {
    public class EqualityToBoolConverter : ValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var areEqual = value == parameter;

            return areEqual;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
