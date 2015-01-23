namespace Alsolos.AttendanceRecorder.WindowsService
{
    using System;
    using System.ServiceProcess;

    public class AttendanceRecorderWindowsService : ServiceBase
    {
        private readonly Worker _worker;

        public AttendanceRecorderWindowsService()
        {
            ServiceName = "Attendance Recorder Service";

            CanHandleSessionChangeEvent = true;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;
            _worker = new Worker(Environment.UserDomainName + "\\" + Environment.UserName);
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            //_worker.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            //_worker.Stop();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _worker.Stop();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            _worker.Start();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            //_worker.Stop();
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
            if (changeDescription.Reason == SessionChangeReason.SessionLock
                || changeDescription.Reason == SessionChangeReason.SessionLogoff)
            {
                _worker.Stop();
            }
            else if (changeDescription.Reason == SessionChangeReason.SessionUnlock
                || changeDescription.Reason == SessionChangeReason.SessionLogon)
            {
                _worker.Start();
            }
        }
    }
}
