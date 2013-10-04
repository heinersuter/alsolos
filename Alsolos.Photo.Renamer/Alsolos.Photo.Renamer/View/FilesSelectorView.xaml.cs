namespace Alsolos.Photo.Renamer.View {
    using System.Windows;

    public partial class FilesSelectorView {
        public FilesSelectorView() {
            InitializeComponent();
        }

        private void OnPreviewDragEnter(object sender, DragEventArgs e) {
            ((FilesSelectorViewModel)DataContext).OnPreviewDragEnter(e);
        }

        private void OnPreviewDrop(object sender, DragEventArgs e) {
            ((FilesSelectorViewModel)DataContext).OnPreviewDrop(e);
        }
    }
}
