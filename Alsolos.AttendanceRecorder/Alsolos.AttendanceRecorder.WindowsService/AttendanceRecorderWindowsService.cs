namespace Alsolos.AttendanceRecorder.WindowsService
{
    using System;
    using System.ServiceProcess;
    using Alsolos.AttendanceRecorder.LocalService;

    public class AttendanceRecorderWindowsService : ServiceBase
    {
        private Worker _worker;

        public AttendanceRecorderWindowsService()
        {
            ServiceName = "Attendance Recorder Service";

            CanHandlePowerEvent = true;
            CanHandleSessionChangeEvent = true;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;
            _worker = new Worker(new AttendanceRecorderService());
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _worker.Start();
        }

        protected override void OnStop()
        {
            _worker.Stop();
            base.OnStop();
        }

        protected override void OnPause()
        {
            _worker.Stop();
            base.OnPause();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            _worker.Start();
        }

        protected override void OnShutdown()
        {
            _worker.Stop();
            base.OnShutdown();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }
    }
}
