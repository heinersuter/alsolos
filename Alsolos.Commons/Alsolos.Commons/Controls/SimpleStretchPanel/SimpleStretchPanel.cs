using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Controls.SimpleStretchPanel {
    public class SimpleStretchPanel : ItemsControl {
        static SimpleStretchPanel() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleStretchPanel), new FrameworkPropertyMetadata(typeof(SimpleStretchPanel)));
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
            "Mode",
            typeof(SimpleStretchPanelMode),
            typeof(SimpleStretchPanel),
            new PropertyMetadata(SimpleStretchPanelMode.VerticalStretchCenter, OnModePropertyChanged));

        public SimpleStretchPanelMode Mode {
            get { return (SimpleStretchPanelMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            CheckMaxNumberOfChildren(this);
            base.OnItemsChanged(e);
        }

        private static void OnModePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
            CheckMaxNumberOfChildren((SimpleStretchPanel)dependencyObject);
        }

        private static void CheckMaxNumberOfChildren(SimpleStretchPanel panel) {
            if (panel.Mode == SimpleStretchPanelMode.HorizontalStretchCenter || panel.Mode == SimpleStretchPanelMode.VerticalStretchCenter) {
                if (panel.Items.Count > 3) {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "For mode '{0}' at most 3 controls are supported.", panel.Mode));
                }
            } else {
                if (panel.Items.Count > 2) {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "For mode '{0}' at most 2 controls are supported.", panel.Mode));
                }
            }
        }
    }
}
