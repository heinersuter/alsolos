using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Alsolos.Commons.Controls.Enabled {
    public static class EnabledControlHelper {
        /// <summary>
        /// Overrides Metadata of the IsEnabled property of the Scrollbar.
        /// </summary>
        public static void AlwaysEnableScrollBars() {
            UIElement.IsEnabledProperty.OverrideMetadata(typeof(ScrollBar), new FrameworkPropertyMetadata(true, null, (source, value) => true));
        }

        public static void SetIsEnabledOnContentAccordingToParent(DependencyObject source) {
            var contentControl = source as ContentControl;
            if (contentControl != null) {
                var parent = FindValidParent(contentControl);
                if (parent != null) {
                    var content = contentControl.Content as UIElement;
                    if (content != null) {
                        content.IsEnabled = parent.IsEnabled;
                    }
                }
            }
        }

        public static void SetIsEnabledOnContentAndHeaderAccordingToParent(DependencyObject source) {
            var contentControl = source as HeaderedContentControl;
            if (contentControl != null) {
                var parent = FindValidParent(contentControl);
                if (parent != null) {
                    var content = contentControl.Content as UIElement;
                    if (content != null) {
                        content.IsEnabled = parent.IsEnabled;
                    }
                    var header = contentControl.Header as UIElement;
                    if (header != null) {
                        header.IsEnabled = parent.IsEnabled;
                    }
                }
            }
        }

        public static void SetIsEnabledOnAllItemsAccordingToParent(DependencyObject source) {
            var itemsControl = source as ItemsControl;
            if (itemsControl != null) {
                var parent = itemsControl.Parent as UIElement;
                if (parent != null) {
                    foreach (var contentControl in itemsControl.Items.OfType<UIElement>()) {
                        contentControl.IsEnabled = parent.IsEnabled;
                    }
                }
            }
        }

        /// <summary>
        /// Finds the first ancestor control that does not implement IEnabledControl.
        /// </summary>
        public static FrameworkElement FindValidParent(FrameworkElement control) {
            if (control == null) {
                return null;
            }

            var enabledControl = control.Parent as IEnabledControl;
            if (enabledControl != null) {
                return FindValidParent(enabledControl as FrameworkElement);
            }
            return control.Parent as FrameworkElement;
        }
    }
}
