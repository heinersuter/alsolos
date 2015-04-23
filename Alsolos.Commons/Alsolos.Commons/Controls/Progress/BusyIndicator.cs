namespace Alsolos.Commons.Controls.Progress
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class BusyIndicator : Control
    {
        private DoubleAnimationUsingKeyFrames _animation;

        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        public BusyIndicator()
        {
            RotateTransform = new RotateTransform(0, 20, 20);
            CreateDiscreteAngleAnimation();

            IsVisibleChanged += OnIsVisibleChanged;
        }

        public static readonly DependencyProperty CircleBrushProperty = DependencyProperty.Register(
            "CircleBrush", typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(Brushes.DodgerBlue));

        public Brush CircleBrush
        {
            get { return (Brush)GetValue(CircleBrushProperty); }
            set { SetValue(CircleBrushProperty, value); }
        }

        public static readonly DependencyProperty RotateTransformProperty = DependencyProperty.Register(
            "RotateTransform", typeof(RotateTransform), typeof(BusyIndicator), new PropertyMetadata(default(RotateTransform)));

        public RotateTransform RotateTransform
        {
            get { return (RotateTransform)GetValue(RotateTransformProperty); }
            set { SetValue(RotateTransformProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(BusyIndicator), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        private void CreateDiscreteAngleAnimation()
        {
            _animation = new DoubleAnimationUsingKeyFrames();
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(036.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(072.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(108.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(144.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(180.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(216.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(252.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(288.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(324.0));
            _animation.KeyFrames.Add(new DiscreteDoubleKeyFrame(360.0));
            _animation.RepeatBehavior = RepeatBehavior.Forever;
            _animation.Duration = new Duration(TimeSpan.FromSeconds(1));
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (IsVisible)
            {
                RotateTransform.BeginAnimation(
                    RotateTransform.AngleProperty, _animation);
            }
            else
            {
                RotateTransform.BeginAnimation(
                    RotateTransform.AngleProperty,
                    new DoubleAnimation(0.0, TimeSpan.FromSeconds(0.0)) { RepeatBehavior = new RepeatBehavior(0) });
            }
        }
    }
}
