namespace Alsolos.Commons.Controls {
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// An expander that is not disabled when its parent is disabled. But header and content of the expander are disabled.
    /// </summary>
    public class EnabledExpander : Expander, IEnabledControl {
        static EnabledExpander() {
            IsEnabledProperty.OverrideMetadata(typeof(EnabledExpander), new FrameworkPropertyMetadata(true, null, OnIsEnabledCoerceValue));
            ContentProperty.OverrideMetadata(typeof(EnabledExpander), new FrameworkPropertyMetadata(OnContentPropertyChanged));
        }

        private static object OnIsEnabledCoerceValue(DependencyObject source, object value) {
            EnabledControlHelper.SetIsEnabledOnContentAndHeaderAccordingToParent(source);
            return true;
        }

        private static void OnContentPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args) {
            EnabledControlHelper.SetIsEnabledOnContentAndHeaderAccordingToParent(source);
        }
    }
}
