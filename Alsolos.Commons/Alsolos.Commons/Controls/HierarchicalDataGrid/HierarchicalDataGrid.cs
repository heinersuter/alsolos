using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Alsolos.Commons.Mvvm;

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

            InputBindings.Add(new KeyBinding(new DelegateCommand(ExpandSelectedRow), Key.Right, ModifierKeys.None));
            InputBindings.Add(new KeyBinding(new DelegateCommand(CollapseSelectedRow), Key.Left, ModifierKeys.None));

            base.OnInitialized(e);
        }

        protected override void OnSorting(DataGridSortingEventArgs eventArgs) {
            if (eventArgs == null) {
                throw new ArgumentException("Arguments must not be null.", "eventArgs");
            }
            var collection = ItemsSource as HierarchicalDataGridItemWrapperCollection;
            if (collection == null) {
                return;
            }
            eventArgs.Column.SortDirection = eventArgs.Column.SortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            collection.Sort(eventArgs.Column.SortMemberPath, eventArgs.Column.SortDirection.Value);
            eventArgs.Handled = true;
        }

        private void ExpandSelectedRow() {
            var selectedWrapper = SelectedItem as HierarchicalDataGridItemWrapper;
            if (selectedWrapper != null) {
                selectedWrapper.IsExpanded = true;
            }
        }

        private void CollapseSelectedRow() {
            var selectedWrapper = SelectedItem as HierarchicalDataGridItemWrapper;
            if (selectedWrapper != null) {
                selectedWrapper.IsExpanded = false;
            }
        }
    }
}
