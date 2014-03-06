using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Controls.Enabled {
    /// <summary>
    /// ContentControl that will not disable the content when the parent control is disabled.
    /// </summary>
    public class EnabledControl : ContentControl, IEnabledControl {
        static EnabledControl() {
            IsEnabledProperty.OverrideMetadata(typeof(EnabledControl), new FrameworkPropertyMetadata(true, null, (sourse, value) => true));
        }
    }
}
