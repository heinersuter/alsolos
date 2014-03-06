using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Alsolos.Commons.Mvvm.Converters {
    public abstract class ValueConverter : MarkupExtension, IValueConverter {
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
