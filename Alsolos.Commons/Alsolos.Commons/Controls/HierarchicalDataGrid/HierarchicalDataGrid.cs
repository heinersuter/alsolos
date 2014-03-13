using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    public class HierarchicalDataGrid : DataGrid {
        protected override void OnInitialized(EventArgs e) {
            // Add column with indented expander buttons
            var resourceDictionary = (ResourceDictionary)Application.LoadComponent(
                new Uri("/Alsolos.Commons;component/Controls/HierarchicalDataGrid/HierarchicalDataGridStyles.xaml", UriKind.RelativeOrAbsolute));
            Columns.Insert(
                0,
                new DataGridTemplateColumn {
                    CellTemplate = (DataTemplate)resourceDictionary["ExpanderCellTemplate"],
                    IsReadOnly = true,
                });

            base.OnInitialized(e);
        }

        protected override void OnSorting(DataGridSortingEventArgs e) {
            var collection = ItemsSource as HierarchicalDataGridItemWrapperCollection;
            if (collection == null) {
                return;
            }

            e.Column.SortDirection = e.Column.SortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            collection.Sort(e.Column.SortMemberPath, e.Column.SortDirection.Value);

            e.Handled = true;
        }
    }
}
