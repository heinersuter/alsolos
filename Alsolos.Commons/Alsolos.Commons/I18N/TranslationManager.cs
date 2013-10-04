namespace Alsolos.Commons.I18N {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    public class TranslationManager {
        private static TranslationManager _translationManager;

        private TranslationManager() {
            var assembly = Assembly.GetEntryAssembly();
            TranslationProvider = new ResxTranslationProvider(assembly.GetName().Name + ".Properties.Texts", assembly);
        }

        public static TranslationManager Instance {
            get { return _translationManager ?? (_translationManager = new TranslationManager()); }
        }

        public CultureInfo CurrentLanguage {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set {
                if (value != Thread.CurrentThread.CurrentUICulture) {
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnLanguageChanged();
                }
            }
        }

        public IEnumerable<CultureInfo> Languages {
            get {
                return TranslationProvider != null ? TranslationProvider.Languages : Enumerable.Empty<CultureInfo>();
            }
        }

        public ITranslationProvider TranslationProvider { get; set; }

        public event EventHandler LanguageChanged;

        public string Translate(string key) {
            if (TranslationProvider != null) {
                var translatedValue = TranslationProvider.Translate(key) as string;
                if (translatedValue != null) {
                    return translatedValue;
                }
            }
            return string.Format(CultureInfo.CurrentCulture, "!{0}!", key);
        }

        public string Format(string key, params string[] translatedArgs) {
            var translatedFormat = Translate(key);
            return string.Format(CultureInfo.CurrentCulture, translatedFormat, translatedArgs);
        }

        private void OnLanguageChanged() {
            if (LanguageChanged != null) {
                LanguageChanged(this, EventArgs.Empty);
            }
        }
    }
}
