namespace Alsolos.Commons.Mvvm.Converters {
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Windows;

    public class ListEmptyToVisibilityConverter : ValueConverter {
        public ListEmptyToVisibilityConverter() {
            EmptyVisibility = Visibility.Collapsed;
            NotEmptyVisibility = Visibility.Visible;
        }

        public Visibility EmptyVisibility { get; set; }
        public Visibility NotEmptyVisibility { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var list = value as IEnumerable;
            var isEmpty = list == null || !list.Cast<object>().Any();

            if (parameter != null) {
                isEmpty = !isEmpty;
            }

            return isEmpty ? EmptyVisibility : NotEmptyVisibility;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
