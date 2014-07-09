namespace AttendanceRecorder.Recorder
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;
    using AttendanceRecorder.Log;
    using AttendanceRecorder.Model;

    public class EventRecorder : DispatcherObject, IDisposable
    {
        private readonly BackgroundWorker _todaysTotalTimeRefreshWorker;

        public LogFile LogFile { get; private set; }

        public EventRecorder()
        {
            LogFile = new LogFile();
            LogFile.EventCollection.Add(SystemState.Down, SystemState.Active);

            var listener = new EventListener();
            listener.StateChanged += OnListenerStateChanged;

            _todaysTotalTimeRefreshWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
            _todaysTotalTimeRefreshWorker.DoWork += OnWorkerDoWork;
            _todaysTotalTimeRefreshWorker.RunWorkerAsync();
        }

        private void OnListenerStateChanged(object sender, StateChangedEventArgs e)
        {
            LogFile.EventCollection.Add(e.OldState, e.NewState);
        }

        private void OnWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            while (e.Cancel == false)
            {
                if (Application.Current != null)
                {
                    Application.Current.Dispatcher.Invoke(new Action(UpdateNowEntry));
                }

                Thread.Sleep(10000);
            }
        }

        private void UpdateNowEntry()
        {
            LogFile.EventCollection.Add(SystemState.Active, SystemState.Now);
        }

        public void Dispose()
        {
            _todaysTotalTimeRefreshWorker.CancelAsync();
            _todaysTotalTimeRefreshWorker.Dispose();

            LogFile.EventCollection.Add(SystemState.Active, SystemState.Down);
        }
    }
}
