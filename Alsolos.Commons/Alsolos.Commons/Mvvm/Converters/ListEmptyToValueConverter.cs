using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace Alsolos.Commons.Mvvm.Converters {
    public class ListEmptyToValueConverter : ValueConverter {
        public object EmptyValue { get; set; }
        public object NotEmptyValue { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var list = value as IEnumerable;
            var isEmpty = list == null || !list.Cast<object>().Any();

            if (parameter != null) {
                isEmpty = !isEmpty;
            }

            return isEmpty ? EmptyValue : NotEmptyValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
