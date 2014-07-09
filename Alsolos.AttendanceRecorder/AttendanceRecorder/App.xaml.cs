namespace AttendanceRecorder
{
    using AttendanceRecorder.View;
    using System.Windows;

    public partial class App : Application
    {
        private MainWindow _window;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _window = new MainWindow();
            _window.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _window.Dispose();
        }
		
		// This is just a test
    }
}
