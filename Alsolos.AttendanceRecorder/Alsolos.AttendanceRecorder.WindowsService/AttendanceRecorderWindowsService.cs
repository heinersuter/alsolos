using System.ServiceProcess;

namespace Alsolos.AttendanceRecorder.WindowsService {
    public class AttendanceRecorderWindowsService : ServiceBase {
        public AttendanceRecorderWindowsService() {
            ServiceName = "Attendance Recorder Service";

            CanHandlePowerEvent = true;
            CanHandleSessionChangeEvent = true;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;
        }

        protected override void OnStart(string[] args) {
            base.OnStart(args);
        }

        protected override void OnStop() {
            base.OnStop();
        }

        protected override void OnPause() {
            base.OnPause();
        }

        protected override void OnContinue() {
            base.OnContinue();
        }

        protected override void OnShutdown() {
            base.OnShutdown();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus) {
            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription) {
            base.OnSessionChange(changeDescription);
        }
    }
}
