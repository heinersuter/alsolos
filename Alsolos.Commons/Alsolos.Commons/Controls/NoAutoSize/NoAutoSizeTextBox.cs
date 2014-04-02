namespace Alsolos.Commons.Controls.NoAutoSize {
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Special implementation of NoAutoSizeDecorator for TextBox.
    /// </summary>
    public class NoAutoSizeTextBox : TextBox {
        static NoAutoSizeTextBox() {
            IsEnabledProperty.OverrideMetadata(
                typeof(NoAutoSizeTextBox),
                new UIPropertyMetadata(
                    true,
                    (source, args) => { },
                    (source, args) => {
                        // Force text box to be always enabled but set IsReadonly
                        // http://social.msdn.microsoft.com/Forums/ar/wpf/thread/3bf5e186-845c-4b63-acd9-17d294a30f77
                        var textBox = (NoAutoSizeTextBox)source;
                        var parent = textBox.Parent as UIElement;
                        if (args as bool? == false || (parent != null && !parent.IsEnabled)) {
                            textBox.IsReadOnly = true;
                            return true;
                        }
                        textBox.IsReadOnly = false;
                        return true;
                    }));
        }

        private static readonly Size _infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);

        private Size _lastFinalSize = _infinitySize;

        public static readonly DependencyProperty KeepWidthProperty = DependencyProperty.Register(
            "KeepWidth", typeof(bool), typeof(NoAutoSizeTextBox), new PropertyMetadata(false));

        public bool KeepWidth {
            get { return (bool)GetValue(KeepWidthProperty); }
            set { SetValue(KeepWidthProperty, value); }
        }

        public static readonly DependencyProperty KeepHeightProperty = DependencyProperty.Register(
            "KeepHeight", typeof(bool), typeof(NoAutoSizeTextBox), new PropertyMetadata(true));

        public bool KeepHeight {
            get { return (bool)GetValue(KeepHeightProperty); }
            set { SetValue(KeepHeightProperty, value); }
        }

        public static readonly DependencyProperty AutoToolTipProperty = DependencyProperty.Register(
            "AutoToolTip", typeof(bool), typeof(NoAutoSizeTextBox), new PropertyMetadata(true));

        public bool AutoToolTip {
            get { return (bool)GetValue(AutoToolTipProperty); }
            set { SetValue(AutoToolTipProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize) {
            var innerWidth = KeepWidth ? availableSize.Width : Math.Min(_lastFinalSize.Width, availableSize.Width);
            var innerHeight = KeepHeight ? availableSize.Height : Math.Min(_lastFinalSize.Height, availableSize.Height);
            var baseSize = base.MeasureOverride(new Size(innerWidth, innerHeight));

            var outerWidth = KeepWidth ? baseSize.Width : 0;
            var outerHeight = KeepHeight ? baseSize.Height : 0;
            return new Size(outerWidth, outerHeight);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            if (_lastFinalSize != finalSize) {
                SetToolTipIfRequired(finalSize);
                _lastFinalSize = finalSize;
                base.MeasureOverride(finalSize);
            }
            return base.ArrangeOverride(finalSize);
        }

        private void SetToolTipIfRequired(Size arrangeBounds) {
            if (!AutoToolTip) {
                return;
            }
            var baseSize = base.MeasureOverride(new Size(double.PositiveInfinity, double.PositiveInfinity));
            ToolTip = baseSize.Width > arrangeBounds.Width ? Text : null;
        }
    }
}
