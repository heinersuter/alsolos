namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class HierarchicalDataGrid : DataGrid {
        public static readonly DependencyProperty HierarchicalDataGridItemsProperty = DependencyProperty.Register(
            "HierarchicalDataGridItems",
            typeof(IEnumerable),
            typeof(HierarchicalDataGrid),
            new FrameworkPropertyMetadata(default(IEnumerable), OnHierarchicalDataGridItemsChanged) {
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                BindsTwoWayByDefault = true,
            });

        public IEnumerable HierarchicalDataGridItems {
            get { return (IEnumerable)GetValue(HierarchicalDataGridItemsProperty); }
            set { SetValue(HierarchicalDataGridItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedWrappersProperty = DependencyProperty.Register(
            "SelectedWrappers",
            typeof(IEnumerable<HierachicalDataGridItemWrapper>),
            typeof(HierarchicalDataGrid),
            new FrameworkPropertyMetadata(null) {
                DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            });

        public IEnumerable<HierachicalDataGridItemWrapper> SelectedWrappers {
            get { return (IEnumerable<HierachicalDataGridItemWrapper>)GetValue(SelectedWrappersProperty); }
            set { SetValue(SelectedWrappersProperty, value); }
        }

        public void ExpandAll() {
            foreach (var item in Items) {
                var wrapper = (HierachicalDataGridItemWrapper)item;
                wrapper.ExpandRecursively();
            }
        }

        public void CollapseAll() {
            foreach (var item in Items) {
                var wrapper = (HierachicalDataGridItemWrapper)item;
                wrapper.CollapseRecursively();
            }
        }

        protected override void OnInitialized(EventArgs e) {
            // Disable sorting for all columns
            foreach (var column in Columns) {
                column.CanUserSort = false;
            }

            // Add column with indented expander buttons
            var resourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri("/ExpandableGridControl;component/Control/HierarchicalDataGrid.xaml", UriKind.RelativeOrAbsolute));
            Columns.Insert(0, new DataGridTemplateColumn { CellTemplate = (DataTemplate)resourceDictionary["ExpanderCellTemplate"] });

            base.OnInitialized(e);
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e) {
            base.OnSelectionChanged(e);
            SelectedWrappers = SelectedItems.Cast<HierachicalDataGridItemWrapper>().ToList();
        }

        private static void OnHierarchicalDataGridItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var grid = (HierarchicalDataGrid)sender;
            var items = (IEnumerable)e.NewValue;
            if (items != null) {
                var dataGridItems = items.Cast<IHierarchicalDataGridItem>();
                grid.AddItems(dataGridItems);
            } else {
                grid.AddItems(null);
            }
        }

        private void AddItems(IEnumerable<IHierarchicalDataGridItem> items) {
            foreach (var item in Items) {
                var wrapper = (HierachicalDataGridItemWrapper)item;
                wrapper.PropertyChanged -= OnWrapperPropertyChanged;
            }
            Items.Clear();

            if (items == null) {
                return;
            }
            foreach (var item in items) {
                var wrapper = HierachicalDataGridItemWrapper.CreateRecursively(item);
                AddItemRecursively(wrapper);
                wrapper.IsParentExpanded = true;
                wrapper.IsExpanded = false;
            }
        }

        private void AddItemRecursively(HierachicalDataGridItemWrapper wrapper) {
            Items.Add(wrapper);
            wrapper.PropertyChanged += OnWrapperPropertyChanged;
            foreach (var child in wrapper.Children) {
                AddItemRecursively(child);
            }
        }

        private void OnWrapperPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsParentExpanded") {
                ApplyFilter();
            }
        }

        private void ApplyFilter() {
            var collectionView = CollectionViewSource.GetDefaultView(Items);
            collectionView.Filter = Filter;
            collectionView.Refresh();
        }

        private static bool Filter(object item) {
            var wrapper = item as HierachicalDataGridItemWrapper;
            return wrapper != null && wrapper.IsParentExpanded;
        }
    }
}
