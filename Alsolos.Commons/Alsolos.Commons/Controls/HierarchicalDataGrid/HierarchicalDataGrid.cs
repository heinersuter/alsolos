using System;
using System.Windows;
using System.Windows.Controls;

namespace Alsolos.Commons.Controls.HierarchicalDataGrid {
    public class HierarchicalDataGrid : DataGrid {
        protected override void OnInitialized(EventArgs e) {
            // Disable sorting for all columns
            foreach (var column in Columns) {
                column.CanUserSort = false;
            }

            // Add column with indented expander buttons
            var resourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri(
                "/Alsolos.Commons;component/Controls/HierarchicalDataGrid/HierarchicalDataGridStyles.xaml",
                UriKind.RelativeOrAbsolute));
            Columns.Insert(
                0,
                new DataGridTemplateColumn {
                    CellTemplate = (DataTemplate)resourceDictionary["ExpanderCellTemplate"],
                    IsReadOnly = true,
                });

            base.OnInitialized(e);
        }
    }
}
