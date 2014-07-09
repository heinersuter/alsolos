namespace AttendanceRecorder.View.Commons
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private readonly Action _executeAction;
        private readonly Func<bool> _canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            _executeAction = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke();
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _executeAction;
        private readonly Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _executeAction = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction.Invoke((T)parameter);
        }
    }
}
