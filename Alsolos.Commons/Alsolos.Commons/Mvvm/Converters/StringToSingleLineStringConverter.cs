namespace Alsolos.Commons.Mvvm.Converters {
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class StringToSingleLineStringConverter : IValueConverter {
        // ↲ ↵ ⤶ ␍ ⏎ ¶
        public const string Space = " ";
        public const string ReturnArrow = "↵";
        public const string Pilcrow = "¶";

        public StringToSingleLineStringConverter() {
            NewLineReplacement = Space;
        }

        public string NewLineReplacement { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var stringValue = value as string;
            if (stringValue == null) {
                return null;
            }
            return stringValue.Replace(Environment.NewLine, NewLineReplacement);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
