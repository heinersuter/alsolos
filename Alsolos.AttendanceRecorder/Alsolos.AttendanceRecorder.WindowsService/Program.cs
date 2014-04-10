using System.ServiceProcess;

namespace Alsolos.AttendanceRecorder.WindowsService {
    public static class Program {
        public static void Main() {
            var servicesToRun = new ServiceBase[] { 
                new AttendanceRecorderWindowsService(),
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}
