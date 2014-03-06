using System;
using System.Globalization;

namespace Alsolos.Commons.Mvvm.Converters {
    public class NullToBoolConverter : ValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isNull = value == null;

            if (parameter != null) {
                isNull = !isNull;
            }

            return isNull;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
