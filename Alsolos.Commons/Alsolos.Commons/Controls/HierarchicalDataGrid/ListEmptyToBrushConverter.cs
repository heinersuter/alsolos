namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ListEmptyToBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var list = value as IList;
            if (list == null) {
                return Brushes.Red;
            }

            if (list.Count > 0) {
                return Brushes.Black;
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
