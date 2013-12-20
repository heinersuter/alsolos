namespace Alsolos.Commons.Controls {
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Idea from <a href="http://stackoverflow.com/questions/198656/nested-scroll-areas">here</a>.
    /// </summary>
    public class NoAutoSizeDecorator : Decorator {
        private static readonly Size _infinitySize = new Size(double.PositiveInfinity, double.PositiveInfinity);

        private Size _lastFinalSize = _infinitySize;

        public static readonly DependencyProperty KeepWidthProperty = DependencyProperty.Register(
            "KeepWidth", typeof(bool), typeof(NoAutoSizeDecorator), new PropertyMetadata(false));

        public bool KeepWidth {
            get { return (bool)GetValue(KeepWidthProperty); }
            set { SetValue(KeepWidthProperty, value); }
        }

        public static readonly DependencyProperty KeepHeightProperty = DependencyProperty.Register(
            "KeepHeight", typeof(bool), typeof(NoAutoSizeDecorator), new PropertyMetadata(false));

        public bool KeepHeight {
            get { return (bool)GetValue(KeepHeightProperty); }
            set { SetValue(KeepHeightProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize) {
            var innerWidth = KeepWidth ? availableSize.Width : Math.Min(_lastFinalSize.Width, availableSize.Width);
            var innerHeight = KeepHeight ? availableSize.Height : Math.Min(_lastFinalSize.Height, availableSize.Height);
            Child.Measure(new Size(innerWidth, innerHeight));

            var outerWidth = KeepWidth ? Child.DesiredSize.Width : 0;
            var outerHeight = KeepHeight ? Child.DesiredSize.Height : 0;
            return new Size(outerWidth, outerHeight);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            if (_lastFinalSize != finalSize) {
                _lastFinalSize = finalSize;
                MeasureOverride(_infinitySize);
            }
            Child.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
