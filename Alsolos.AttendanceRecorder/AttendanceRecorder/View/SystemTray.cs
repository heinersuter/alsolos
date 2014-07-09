namespace AttendanceRecorder.View
{
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows;
    using System.Windows.Forms;

    using AttendanceRecorder.View.Commons;

    public class SystemTray : ViewModel, IDisposable
    {
        private NotifyIcon _notifyIcon;

        private WindowState _windowState;

        public WindowState WindowState
        {
            get
            {
                return _windowState;
            }

            set
            {
                ShowInTaskbar = true;
                if (ChangeAndNotify(ref _windowState, value, () => WindowState))
                {
                    if (WindowState == WindowState.Minimized)
                    {
                        new Action(() =>
                        {
                            Thread.Sleep(200);
                            ShowInTaskbar = false;
                        }).BeginInvoke(null, null);
                    }
                }
            }
        }

        private bool _showInTaskbar;

        public bool ShowInTaskbar
        {
            get { return _showInTaskbar; }
            set { ChangeAndNotify(ref _showInTaskbar, value, () => ShowInTaskbar); }
        }

        public bool DoTErminateDirectly { get; set; }

        public event EventHandler TerminateRequested = delegate { };

        public SystemTray()
        {
            ShowInTaskbar = true;

            _notifyIcon = new NotifyIcon
            {
                Text = "Attendance Recorder",
            };
            using (var stream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/AttendanceRecorder.ico")).Stream)
            {
                this._notifyIcon.Icon = new Icon(stream);
            }

            _notifyIcon.Click += OnNotifyIconClick;

            _notifyIcon.ContextMenu = new ContextMenu(new[] { new MenuItem("Terminate", OnTerminateClick) });

            _notifyIcon.Visible = true;
        }

        private void OnTerminateClick(object sender, EventArgs eventArgs)
        {
            DoTErminateDirectly = true;
            TerminateRequested.Invoke(this, EventArgs.Empty);
        }

        private void OnNotifyIconClick(object sender, EventArgs e)
        {
            if (WindowState != WindowState.Normal)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Minimized;
            }
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }
    }
}
