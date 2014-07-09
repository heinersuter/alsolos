namespace AttendanceRecorder.View
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using AttendanceRecorder.Model;
    using AttendanceRecorder.Recorder;
    using AttendanceRecorder.View.About;
    using AttendanceRecorder.View.Commons;

    public class MainWindowViewModel : ViewModel, IDisposable
    {
        private readonly EventRecorder _recorder;

        public MainWindowViewModel()
        {
            ShowAboutDialogCommand = new DelegateCommand(() => new AboutDialog().ShowDialog());

            _recorder = new EventRecorder();
            DeleteCommand = new DelegateCommand<EventItem>(Delete);
            Days = _recorder.LogFile.EventCollection.Days;

            if (!Debugger.IsAttached)
            {
                SystemTray = new SystemTray { WindowState = WindowState.Minimized };
            }
        }

        public SystemTray SystemTray { get; set; }

        // TODO: Move DeleteCommand to class EventItem
        public DelegateCommand<EventItem> DeleteCommand { get; private set; }

        public DelegateCommand ShowAboutDialogCommand { get; private set; }

        public ObservableCollection<Day> Days { get; private set; }

        public void OnWindowClosing(CancelEventArgs e)
        {
            if (!Debugger.IsAttached)
            {
                if ((!SystemTray.DoTErminateDirectly) && (new CloseDialog().ShowDialog() != true))
                {
                    e.Cancel = true;
                    SystemTray.WindowState = WindowState.Minimized;
                }
            }
        }

        private void Delete(EventItem item)
        {
            _recorder.LogFile.EventCollection.Delete(item);
        }

        public void Dispose()
        {
            _recorder.Dispose();
            if (SystemTray != null)
            {
                SystemTray.Dispose();
            }
        }
    }
}
