namespace AttendanceRecorder.View
{
    using System.Windows;

    public partial class CloseDialog
    {
        public CloseDialog()
        {
            InitializeComponent();
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TerminateClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
