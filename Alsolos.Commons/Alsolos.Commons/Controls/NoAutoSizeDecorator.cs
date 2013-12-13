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
            get { return (bool)this.GetValue(KeepWidthProperty); }
            set { this.SetValue(KeepWidthProperty, value); }
        }

        public static readonly DependencyProperty KeepHeightProperty = DependencyProperty.Register(
            "KeepHeight", typeof(bool), typeof(NoAutoSizeDecorator), new PropertyMetadata(false));

        public bool KeepHeight {
            get { return (bool)this.GetValue(KeepHeightProperty); }
            set { this.SetValue(KeepHeightProperty, value); }
        }

        protected override Size MeasureOverride(Size availableSize) {
            var innerWidth = this.KeepWidth ? availableSize.Width : Math.Min(this._lastFinalSize.Width, availableSize.Width);
            var innerHeight = this.KeepHeight ? availableSize.Height : Math.Min(this._lastFinalSize.Height, availableSize.Height);
            Child.Measure(new Size(innerWidth, innerHeight));

            var outerWidth = this.KeepWidth ? Child.DesiredSize.Width : 0;
            var outerHeight = this.KeepHeight ? Child.DesiredSize.Height : 0;
            return new Size(outerWidth, outerHeight);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            if (_lastFinalSize != finalSize) {
                _lastFinalSize = finalSize;
                this.MeasureOverride(_infinitySize);
            }
            Child.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
