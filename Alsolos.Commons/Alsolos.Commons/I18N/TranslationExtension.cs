namespace Alsolos.Commons.I18N {
    using System;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class TranslateExtension : MarkupExtension {
        private string _key;
        private string _stringFormat;

        public TranslateExtension(string key)
            : this(key, null) {
        }

        public TranslateExtension(string key, string stringFormat) {
            _key = key;
            _stringFormat = stringFormat;
        }

        [ConstructorArgument("key")]
        public string Key {
            get { return _key; }
            set { _key = value; }
        }

        [ConstructorArgument("stringFormat")]
        public string StringFormat {
            get { return _stringFormat; }
            set { _stringFormat = value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var binding = new Binding("Value") {
                Source = new TranslationData(_key, _stringFormat)
            };
            return binding.ProvideValue(serviceProvider);
        }
    }
}
