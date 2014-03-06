using System;
using System.Globalization;
using System.Windows;

namespace Alsolos.Commons.Mvvm.Converters {
    public class EqualityToVisibilityConverter : ValueConverter {
        public EqualityToVisibilityConverter() {
            EqualVisibility = Visibility.Visible;
            NotEqualVisibility = Visibility.Collapsed;
        }

        public Visibility EqualVisibility { get; set; }
        public Visibility NotEqualVisibility { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var areEqual = value == parameter;

            return areEqual ? EqualVisibility : NotEqualVisibility;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
