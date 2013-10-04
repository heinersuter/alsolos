using System;
using System.Windows;

namespace Alsolos.Commons.Mvvm {
    /// <summary>
    /// AttachedProperty to bind the actual size of a control back to the view model.
    /// <a href="http://stackoverflow.com/questions/1083224/pushing-read-only-gui-properties-back-into-viewmodel">See at StackOverflow.</a>
    /// </summary>
    public static class SizeObserver {
        public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached(
            "Observe",
            typeof(bool),
            typeof(SizeObserver),
            new FrameworkPropertyMetadata(OnObserveChanged));

        public static readonly DependencyProperty ObservedWidthProperty = DependencyProperty.RegisterAttached(
            "ObservedWidth",
            typeof(double),
            typeof(SizeObserver));

        public static readonly DependencyProperty ObservedHeightProperty = DependencyProperty.RegisterAttached(
            "ObservedHeight",
            typeof(double),
            typeof(SizeObserver));

        public static bool GetObserve(DependencyObject dependencyObject) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            return (bool)dependencyObject.GetValue(ObserveProperty);
        }

        public static void SetObserve(DependencyObject dependencyObject, bool observe) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            dependencyObject.SetValue(ObserveProperty, observe);
        }

        public static double GetObservedWidth(DependencyObject dependencyObject) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            return (double)dependencyObject.GetValue(ObservedWidthProperty);
        }

        public static void SetObservedWidth(DependencyObject dependencyObject, double observedWidth) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            dependencyObject.SetValue(ObservedWidthProperty, observedWidth);
        }

        public static double GetObservedHeight(DependencyObject dependencyObject) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            return (double)dependencyObject.GetValue(ObservedHeightProperty);
        }

        public static void SetObservedHeight(DependencyObject dependencyObject, double observedHeight) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            dependencyObject.SetValue(ObservedHeightProperty, observedHeight);
        }

        private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            if (dependencyObject == null) {
                throw new ArgumentNullException("dependencyObject");
            }
            var frameworkElement = (FrameworkElement)dependencyObject;

            if ((bool)e.NewValue) {
                frameworkElement.SizeChanged += OnFrameworkElementSizeChanged;
                UpdateObservedSizesForFrameworkElement(frameworkElement);
            } else {
                frameworkElement.SizeChanged -= OnFrameworkElementSizeChanged;
            }
        }

        private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateObservedSizesForFrameworkElement((FrameworkElement)sender);
        }

        private static void UpdateObservedSizesForFrameworkElement(FrameworkElement frameworkElement) {
            frameworkElement.SetCurrentValue(ObservedWidthProperty, frameworkElement.ActualWidth);
            frameworkElement.SetCurrentValue(ObservedHeightProperty, frameworkElement.ActualHeight);
        }
    }
}
