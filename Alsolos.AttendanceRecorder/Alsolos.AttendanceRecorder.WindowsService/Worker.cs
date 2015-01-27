namespace Alsolos.AttendanceRecorder.WindowsService
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using Alsolos.AttendanceRecorder.LocalService;

    public class Worker : IDisposable
    {
        private readonly TimeSpan _intervalDuration = new TimeSpan(0, 0, 10);
        private readonly ManualResetEvent _runEvent = new ManualResetEvent(false);
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private readonly AttendanceRecorderService _service;
        private readonly string _timeAccountname;

        public Worker(string timeAccountname)
        {
            _timeAccountname = timeAccountname;
            _service = new AttendanceRecorderService();
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        public void Start()
        {
            _runEvent.Set();
        }

        public void Stop()
        {
            _runEvent.Reset();
        }

        public void Dispose()
        {
            if (_service != null)
            {
                _service.Dispose();
            }
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            while (true)
            {
                if (_runEvent.WaitOne())
                {
                    _service.KeepAlive(_timeAccountname, _intervalDuration);
                }
                Thread.Sleep(_intervalDuration);
            }
        }
    }
}
