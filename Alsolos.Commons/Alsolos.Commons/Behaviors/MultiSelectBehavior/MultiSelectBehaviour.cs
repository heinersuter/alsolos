namespace Alsolos.Commons.Behaviors.MultiselectBehavior {
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Published <a href="http://blog.functionalfun.net/2009/02/how-to-databind-to-selecteditems.html">on the web</a> by Samuel Jack.
    /// </summary>
    public static class MultiselectBehaviour {
        public static readonly DependencyProperty SelectedItems = DependencyProperty.RegisterAttached(
            "SelectedItems", typeof(IList), typeof(MultiselectBehaviour), new PropertyMetadata(null, OnSelectedItemsChanged));

        private static readonly DependencyProperty SynchronizationManagerProperty = DependencyProperty.RegisterAttached(
            "SynchronizationManager", typeof(SynchronizationManager), typeof(MultiselectBehaviour), new PropertyMetadata(null));

        public static IList GetSelectedItems(DependencyObject dependencyObject) {
            return (IList)dependencyObject.GetValue(SelectedItems);
        }

        public static void SetSelectedItems(DependencyObject dependencyObject, IList value) {
            dependencyObject.SetValue(SelectedItems, value);
        }

        private static SynchronizationManager GetSynchronizationManager(DependencyObject dependencyObject) {
            return (SynchronizationManager)dependencyObject.GetValue(SynchronizationManagerProperty);
        }

        private static void SetSynchronizationManager(DependencyObject dependencyObject, SynchronizationManager value) {
            dependencyObject.SetValue(SynchronizationManagerProperty, value);
        }

        private static void OnSelectedItemsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            if (e.OldValue != null) {
                var synchronizer = GetSynchronizationManager(dependencyObject);
                synchronizer.StopSynchronizing();

                SetSynchronizationManager(dependencyObject, null);
            }

            var list = e.NewValue as IList;
            var selector = dependencyObject as Selector;

            // check that this property is an IList, and that it is being set on a ListBox
            if (list != null && selector != null) {
                var synchronizer = GetSynchronizationManager(dependencyObject);
                if (synchronizer == null) {
                    synchronizer = new SynchronizationManager(selector);
                    SetSynchronizationManager(dependencyObject, synchronizer);
                }

                synchronizer.StartSynchronizingList();
            }
        }
    }
}
