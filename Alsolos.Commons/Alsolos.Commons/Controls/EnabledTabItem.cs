using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Controls {
    /// <summary>
    /// A TabItem that is not disabled when its parent is disabled. But the content will be disabled.
    /// Use only inside EnabledTabControl.
    /// </summary>
    public class EnabledTabItem : TabItem, IEnabledControl {
        static EnabledTabItem() {
            IsEnabledProperty.OverrideMetadata(typeof(EnabledTabItem), new FrameworkPropertyMetadata(true, null, OnIsEnabledCoerceValue));
            ContentProperty.OverrideMetadata(typeof(EnabledTabItem), new FrameworkPropertyMetadata(OnContentPropertyChanged));
        }

        private static object OnIsEnabledCoerceValue(DependencyObject source, object value) {
            EnabledControlHelper.SetIsEnabledOnContentAccordingToParent(source);
            return true;
        }

        private static void OnContentPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args) {
            EnabledControlHelper.SetIsEnabledOnContentAccordingToParent(source);
        }
    }
}
