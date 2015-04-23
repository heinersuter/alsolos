namespace Alsolos.Commons.Controls.Progress
{
    using System.Windows;
    using System.Windows.Controls;

    public class BusyContentControl : ContentControl
    {
        static BusyContentControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyContentControl), new FrameworkPropertyMetadata(typeof(BusyContentControl)));
        }

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy", typeof(bool), typeof(BusyContentControl), new PropertyMetadata(default(bool)));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(BusyContentControl), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
    }
}
