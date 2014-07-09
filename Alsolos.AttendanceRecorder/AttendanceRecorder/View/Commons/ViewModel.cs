namespace AttendanceRecorder.View.Commons
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected bool ChangeAndNotify<T>(ref T field, T newValue, Expression<Func<T>> propertyExpression)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyExpression.Name));
                return true;
            }
            return false;
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
        {
            var propertyName = PropertyHelper.ExtractName(propertyExpresssion);
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
