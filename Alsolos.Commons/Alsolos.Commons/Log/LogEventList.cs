namespace Alsolos.Commons.Log
{
    using System.Windows;
    using System.Windows.Controls;

    public class LogEventList : Control
    {
        static LogEventList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LogEventList), new FrameworkPropertyMetadata(typeof(LogEventList)));
        }
    }
}
