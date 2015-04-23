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

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(BusyContentControl), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
    }
}
