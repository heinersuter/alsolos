namespace AttendanceRecorder.View
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Markup;

    public partial class MainWindow : IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            var viewModel = DataContext as MainWindowViewModel;
            if ((viewModel != null) && (viewModel.SystemTray != null))
            {
                viewModel.SystemTray.TerminateRequested += (source, args) => Close();
            }
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.OnWindowClosing(e);
            }
        }

        public void Dispose()
        {
            var viewModel = DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.Dispose();
            }
        }
    }
}
