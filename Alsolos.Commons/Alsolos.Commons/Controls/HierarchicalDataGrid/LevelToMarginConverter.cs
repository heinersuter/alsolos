namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class LevelToMarginConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is int)) {
                return new Thickness(0);
            }
            var level = (int)value;
            return new Thickness(level * 12, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
