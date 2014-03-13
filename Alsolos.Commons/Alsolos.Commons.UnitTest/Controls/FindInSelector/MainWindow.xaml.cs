using System.Windows.Controls;

namespace Alsolos.Commons.UnitTest.Controls.FindInSelector {
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void OnSelectorSelectionChanged(object sender, SelectionChangedEventArgs e) {
            var dataGrid = (DataGrid)sender;
            if (dataGrid.SelectedItem != null) {
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
        }
    }
}
