using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Controls.Enabled {
    /// <summary>
    /// A TabControl that is not disabled when its parent is disabled. Must use EnabledTabItem as children.
    /// </summary>
    public class EnabledTabControl : TabControl, IEnabledControl {
        static EnabledTabControl() {
            IsEnabledProperty.OverrideMetadata(typeof(EnabledTabControl), new FrameworkPropertyMetadata(true, null, OnIsEnabledCoerceValue));
            ContentControl.ContentProperty.OverrideMetadata(typeof(EnabledTabControl), new FrameworkPropertyMetadata(OnContentPropertyChanged));
        }

        private static object OnIsEnabledCoerceValue(DependencyObject source, object value) {
            EnabledControlHelper.SetIsEnabledOnAllItemsAccordingToParent(source);
            return true;
        }

        private static void OnContentPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args) {
            EnabledControlHelper.SetIsEnabledOnAllItemsAccordingToParent(source);
        }
    }
}
