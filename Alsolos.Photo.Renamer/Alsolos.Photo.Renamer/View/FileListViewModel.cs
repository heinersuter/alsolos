namespace Alsolos.Photo.Renamer.View {
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.Model;
    using Alsolos.Photo.Renamer.Properties;
    using Alsolos.Photo.Renamer.Services;
    using Ookii.Dialogs.Wpf;

    public class FileListViewModel : ViewModel {
        private readonly FileSystemService _fileSystemService = new FileSystemService();

        public FileListViewModel(FileRenameViewModel fileRenameViewModel) {
            FileRenameViewModel = fileRenameViewModel;
            ConnectIsBusy(fileRenameViewModel);
        }

        public FileRenameViewModel FileRenameViewModel { get; private set; }

        public ObservableCollection<FileWrapper> AllFiles {
            get { return BackingFields.GetValue(() => AllFiles); }
            set {
                if (AllFiles != null) {
                    AllFiles.CollectionChanged -= OnFilesCollectionChanged;
                }
                if (value != null) {
                    value.CollectionChanged += OnFilesCollectionChanged;
                }
                if (BackingFields.SetValue(() => AllFiles, value)) {
                    RaisePropertyChanged(() => FilesCountText);
                }
            }
        }

        public FileWrapper SelectedFile {
            get { return BackingFields.GetValue(() => SelectedFile); }
            set { BackingFields.SetValue(() => SelectedFile, value); }
        }

        public string FilesCountText {
            get {
                var filesCount = AllFiles != null ? AllFiles.Count : 0;
                return filesCount != 1
                    ? string.Format(CultureInfo.CurrentCulture, Texts.FileListCountMany, filesCount)
                    : Texts.FileListCountSingle;
            }
        }

        public DelegateCommand SelectFilesCommand {
            get { return BackingFields.GetCommand(() => SelectFilesCommand, SelectFiles, CanSelectFileOrFolder); }
        }

        public DelegateCommand SelectFolderCommand {
            get { return BackingFields.GetCommand(() => SelectFolderCommand, SelectFolder, CanSelectFileOrFolder); }
        }

        public DelegateCommand DeleteSelectedFileCommand {
            get { return BackingFields.GetCommand(() => DeleteSelectedFileCommand, DeleteSelectedFile); }
        }

        public void OnPreviewDragEnter(DragEventArgs e) {
            if (!IsBusy && e.Data.GetDataPresent(DataFormats.FileDrop)) {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (fileNames != null && fileNames.Any(s => {
                    var extension = Path.GetExtension(s);
                    return extension != null && extension.ToLowerInvariant() == ".jpg";
                })) {
                    e.Effects = DragDropEffects.All;
                    e.Handled = true;
                    return;
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        public void OnPreviewDrop(DragEventArgs e) {
            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (fileNames != null && fileNames.Any()) {
                var imageFileNames = fileNames.Where(fileName => {
                    var extension = Path.GetExtension(fileName);
                    return extension != null && extension.ToLowerInvariant() == ".jpg";
                });
                AllFiles = new ObservableCollection<FileWrapper>(imageFileNames.Select(fileName => new FileWrapper { FullName = fileName }));
            }
        }

        private void OnFilesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            RaisePropertyChanged(() => FilesCountText);
        }

        private bool CanSelectFileOrFolder() {
            return !IsBusy;
        }

        private void SelectFiles() {
            var dialog = new VistaOpenFileDialog {
                Multiselect = true,
                Filter = @"JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*",
            };
            if (dialog.ShowDialog() == true) {
                AllFiles = new ObservableCollection<FileWrapper>(dialog.FileNames.Select(s => new FileWrapper { FullName = s }));
            }
        }

        private void SelectFolder() {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true) {
                var fileNames = _fileSystemService.GetAllFilesInDirectoryWithExtension(dialog.SelectedPath, ".jpg");
                AllFiles = new ObservableCollection<FileWrapper>(fileNames.Select(fileName => new FileWrapper { FullName = fileName }));
            }
        }

        private void DeleteSelectedFile() {
            if (AllFiles != null && SelectedFile != null) {
                AllFiles.Remove(SelectedFile);
            }
        }
    }
}
