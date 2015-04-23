namespace Alsolos.Commons.Controls.Progress
{
    using System;
    using Alsolos.Commons.Mvvm;

    public class BusyState : BackingFieldsHolder, IDisposable
    {
        private Action _callback;

        public BusyState(Action callback)
        {
            _callback = callback;
        }

        public void Dispose()
        {
            _callback.Invoke();
            _callback = null;
        }
    }
}
