namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class HierarchicalDataGrid : DataGrid {
        public static readonly DependencyProperty HierarchicalDataGridItemsProperty = DependencyProperty.Register(
            "HierarchicalDataGridItems",
            typeof(IList<IHierarchicalDataGridItem>),
            typeof(HierarchicalDataGrid),
            new PropertyMetadata(new List<IHierarchicalDataGridItem>(), OnHierarchicalDataGridItemsChanged));

        public IList<IHierarchicalDataGridItem> HierarchicalDataGridItems {
            get { return (IList<IHierarchicalDataGridItem>)this.GetValue(HierarchicalDataGridItemsProperty); }
            set { this.SetValue(HierarchicalDataGridItemsProperty, value); }
        }

        private static void OnHierarchicalDataGridItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            var grid = (HierarchicalDataGrid)sender;
            grid.AddItems((IList<IHierarchicalDataGridItem>)e.NewValue);
        }

        protected override void OnInitialized(EventArgs e) {
            // Disable sorting for all columns
            foreach (var column in this.Columns) {
                column.CanUserSort = false;
            }

            // Add column with indented expander buttons
            var resourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri("/ExpandableGridControl;component/Control/HierarchicalDataGrid.xaml", UriKind.RelativeOrAbsolute));
            this.Columns.Insert(0, new DataGridTemplateColumn { CellTemplate = (DataTemplate)resourceDictionary["ExpanderCellTemplate"] });

            base.OnInitialized(e);
        }

        public void ExpandAll() {
            foreach (var item in this.Items) {
                var wrapper = (HierachicalDataGridItemWrapper)item;
                wrapper.ExpandRecursively();
            }
        }

        public void CollapseAll() {
            foreach (var item in this.Items) {
                var wrapper = (HierachicalDataGridItemWrapper)item;
                wrapper.CollapseRecursively();
            }
        }

        private void AddItems(IEnumerable<IHierarchicalDataGridItem> items) {
            foreach (var item in this.Items) {
                var wrapper = (HierachicalDataGridItemWrapper)item;
                wrapper.PropertyChanged -= this.OnWrapperPropertyChanged;
            }
            this.Items.Clear();

            if (items == null) {
                return;
            }
            foreach (var item in items) {
                var wrapper = HierachicalDataGridItemWrapper.CreateRecursively(item);
                this.AddItemRecursively(wrapper);
                wrapper.IsParentExpanded = true;
                wrapper.IsExpanded = false;
            }
        }

        private void AddItemRecursively(HierachicalDataGridItemWrapper wrapper) {
            this.Items.Add(wrapper);
            wrapper.PropertyChanged += this.OnWrapperPropertyChanged;
            foreach (var child in wrapper.Children) {
                this.AddItemRecursively(child);
            }
        }

        private void OnWrapperPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == "IsParentExpanded") {
                this.ApplyFilter();
            }
        }

        private void ApplyFilter() {
            var collectionView = CollectionViewSource.GetDefaultView(this.Items);
            collectionView.Filter = Filter;
            collectionView.Refresh();
        }

        private static bool Filter(object item) {
            var wrapper = item as HierachicalDataGridItemWrapper;
            return wrapper != null && wrapper.IsParentExpanded;
        }
    }
}
