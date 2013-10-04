using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Alsolos.Commons.Mvvm {
    public class BackingFieldsHolder : INotifyPropertyChanged {
        private static BackingFields _staticBackingFields;

        private BackingFields _backingFields;

        public BackingFieldsHolder() {
            PropertyChanged += OnPropertyChanged;
        }

        public static BackingFields StaticBackingFields {
            get { return PropertyHelper.CreateIfNeeded(ref _staticBackingFields, () => new BackingFields(null)); }
        }

        public BackingFields BackingFields {
            get { return PropertyHelper.CreateIfNeeded(ref _backingFields, () => new BackingFields(RaisePropertyChanged)); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public T CreateIfNeeded<T>(Expression<Func<T>> propertyExpression, Func<T> newInstanceCreateMethod) {
            if (newInstanceCreateMethod == null) {
                return default(T);
            }
            if (Equals(BackingFields.GetValue(propertyExpression), default(T))) {
                BackingFields.SetValue(propertyExpression, newInstanceCreateMethod.Invoke());
            }
            return BackingFields.GetValue(propertyExpression);
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression) {
            return PropertyHelper.GetName(propertyExpression);
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            var propertyName = GetPropertyName(propertyExpression);
            var copy = PropertyChanged;
            if (copy != null) {
                copy.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
