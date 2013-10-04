namespace Alsolos.Commons.Mvvm {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class BackingFields {
        private readonly Dictionary<string, object> _properties;
        private readonly Dictionary<string, PropertyChangedEventHandler> _observers;

        private readonly Action<string> _propertyChangedEventHandler;

        public BackingFields(Action<string> propertyChangedEventHandler) {
            _properties = new Dictionary<string, object>();
            _observers = new Dictionary<string, PropertyChangedEventHandler>();
            _propertyChangedEventHandler = propertyChangedEventHandler;
        }

        public T GetValue<T>(Expression<Func<T>> propertyExpression, Func<T> initializeFunction = null) {
            object value;
            if (_properties.TryGetValue(GetPropertyName(propertyExpression), out value)) {
                return (T)value;
            }
            if (initializeFunction != null) {
                var initialValue = initializeFunction.Invoke();
                _properties[GetPropertyName(propertyExpression)] = initialValue;
                return initialValue;
            }
            return default(T);
        }

        public T GetValueAndObserve<T>(Expression<Func<T>> propertyExpression, Func<T> initializeFunction, PropertyChangedEventHandler observer) where T : class, INotifyPropertyChanged {
            object value;
            if (_properties.TryGetValue(GetPropertyName(propertyExpression), out value)) {
                return (T)value;
            }
            if (initializeFunction != null) {
                var initialValue = initializeFunction.Invoke();
                if (initialValue != null) {
                    initialValue.PropertyChanged += observer;
                }
                _properties[GetPropertyName(propertyExpression)] = initialValue;
                return initialValue;
            }
            return default(T);
        }

        public T GetValueAndObserve<T>(Expression<Func<T>> propertyExpression, Func<T> initializeFunction) where T : class, INotifyPropertyChanged {
            return GetValueAndObserve(propertyExpression, initializeFunction, GetObserver(propertyExpression));
        }

        public DelegateCommand GetCommand(Expression<Func<DelegateCommand>> propertyExpression, Action executeMethod, Func<bool> canExecuteMethod = null) {
            if (canExecuteMethod == null) {
                canExecuteMethod = () => true;
            }
            return GetValue(propertyExpression, () => new DelegateCommand(executeMethod, canExecuteMethod));
        }

        public bool SetValue<T>(Expression<Func<T>> propertyExpression, T value, Action<T> propertyChangedCallback = null) {
            var previousValue = GetValue(propertyExpression);

            if (!Equals(previousValue, value)) {
                _properties[GetPropertyName(propertyExpression)] = value;
                RaisePropertyChanged(propertyExpression);
                if (propertyChangedCallback != null) {
                    propertyChangedCallback.Invoke(value);
                }
                return true;
            }
            return false;
        }

        public bool SetValueAndObserve<T>(Expression<Func<T>> propertyExpression, T value, PropertyChangedEventHandler observer) where T : class, INotifyPropertyChanged {
            var previousValue = GetValue(propertyExpression);

            if (previousValue != value) {
                if (observer != null) {
                    if (previousValue != null) {
                        previousValue.PropertyChanged -= observer;
                    }
                    if (value != null) {
                        value.PropertyChanged += observer;
                    }
                }
                _properties[GetPropertyName(propertyExpression)] = value;
                RaisePropertyChanged(propertyExpression);
                return true;
            }
            return false;
        }

        public bool SetValueAndObserve<T>(Expression<Func<T>> propertyExpression, T value) where T : class, INotifyPropertyChanged {
            return SetValueAndObserve(propertyExpression, value, GetObserver(propertyExpression));
        }

        private PropertyChangedEventHandler GetObserver<T>(Expression<Func<T>> propertyExpression) {
            return GetObserver(propertyExpression, (sender, args) => RaisePropertyChanged(propertyExpression));
        }

        private PropertyChangedEventHandler GetObserver<T>(Expression<Func<T>> propertyExpression, PropertyChangedEventHandler initialObserver) {
            PropertyChangedEventHandler observer;
            if (_observers.TryGetValue(GetPropertyName(propertyExpression), out observer)) {
                return observer;
            }
            _observers[GetPropertyName(propertyExpression)] = initialObserver;
            return initialObserver;
        }

        private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression) {
            return PropertyHelper.GetName(propertyExpression);
        }

        private void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) {
            if (_propertyChangedEventHandler != null) {
                _propertyChangedEventHandler.Invoke(GetPropertyName(propertyExpression));
            }
        }
    }
}
