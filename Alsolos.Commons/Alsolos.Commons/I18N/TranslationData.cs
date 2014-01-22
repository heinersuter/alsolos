namespace Alsolos.Commons.I18N {
    using System;
    using System.ComponentModel;
    using System.Windows;

    public class TranslationData : IWeakEventListener, INotifyPropertyChanged, IDisposable {
        private readonly string _key;
        private readonly string _stringFormat;

        public TranslationData(string key, string stringFormat) {
            _key = key;
            _stringFormat = stringFormat;
            LanguageChangedEventManager.AddListener(TranslationManager.Instance, this);
        }

        ~TranslationData() {
            Dispose(false);
        }

        public object Value {
            get {
                if (_stringFormat == null) {
                    return TranslationManager.Instance.Translate(_key);
                }
                return string.Format(_stringFormat, TranslationManager.Instance.Translate(_key));
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e) {
            if (managerType == typeof(LanguageChangedEventManager)) {
                OnLanguageChanged();
                return true;
            }
            return false;
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                LanguageChangedEventManager.RemoveListener(TranslationManager.Instance, this);
            }
        }

        private void OnLanguageChanged() {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
