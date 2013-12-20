namespace Alsolos.Photo.Renamer.View {
    using System.Windows;

    public partial class FileListView {
        public FileListView() {
            InitializeComponent();
        }

        private void OnPreviewDragEnter(object sender, DragEventArgs e) {
            ((FileListViewModel)DataContext).OnPreviewDragEnter(e);
        }

        private void OnPreviewDrop(object sender, DragEventArgs e) {
            ((FileListViewModel)DataContext).OnPreviewDrop(e);
        }
    }
}
