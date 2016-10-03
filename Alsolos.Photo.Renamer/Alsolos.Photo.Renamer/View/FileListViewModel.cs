using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using Alsolos.Commons.Wpf.Controls.Progress;
using Alsolos.Commons.Wpf.Mvvm;
using Alsolos.Photo.Renamer.Model;
using Alsolos.Photo.Renamer.Properties;
using Alsolos.Photo.Renamer.Services;
using Ookii.Dialogs.Wpf;

namespace Alsolos.Photo.Renamer.View
{
    public class FileListViewModel : BusyViewModel
    {
        private readonly FileSystemService _fileSystemService = new FileSystemService();

        public FileListViewModel(FileRenameViewModel fileRenameViewModel)
        {
            FileRenameViewModel = fileRenameViewModel;
            //ConnectIsBusy(fileRenameViewModel);
        }

        public FileRenameViewModel FileRenameViewModel { get; private set; }

        public ObservableCollection<FileWrapper> AllFiles
        {
            get { return BackingFields.GetValue<ObservableCollection<FileWrapper>>(); }
            set
            {
                if (AllFiles != null)
                {
                    AllFiles.CollectionChanged -= OnFilesCollectionChanged;
                }
                if (value != null)
                {
                    value.CollectionChanged += OnFilesCollectionChanged;
                }
                if (BackingFields.SetValue(value))
                {
                    RaisePropertyChanged(() => FilesCountText);
                }
            }
        }

        public FileWrapper SelectedFile
        {
            get { return BackingFields.GetValue<FileWrapper>(); }
            set { BackingFields.SetValue(value); }
        }

        public string FilesCountText
        {
            get
            {
                var filesCount = AllFiles?.Count ?? 0;
                return filesCount != 1
                    ? string.Format(CultureInfo.CurrentCulture, Texts.FileListCountMany, filesCount)
                    : Texts.FileListCountSingle;
            }
        }

        public DelegateCommand SelectFilesCommand => BackingFields.GetCommand(SelectFiles, CanSelectFileOrFolder);

        public DelegateCommand SelectFolderCommand => BackingFields.GetCommand(SelectFolder, CanSelectFileOrFolder);

        public DelegateCommand DeleteSelectedFileCommand => BackingFields.GetCommand(DeleteSelectedFile);

        public void OnPreviewDragEnter(DragEventArgs e)
        {
            if (!BusyHelper.IsBusy && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                if ((fileNames != null) && fileNames.Any(s =>
                    {
                        var extension = Path.GetExtension(s);
                        return (extension != null) && (extension.ToLowerInvariant() == ".jpg");
                    }))
                {
                    e.Effects = DragDropEffects.All;
                    e.Handled = true;
                    return;
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        public void OnPreviewDrop(DragEventArgs e)
        {
            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            if ((fileNames != null) && fileNames.Any())
            {
                var imageFileNames = fileNames.Where(fileName =>
                {
                    var extension = Path.GetExtension(fileName);
                    return (extension != null) && (extension.ToLowerInvariant() == ".jpg");
                });
                AllFiles = new ObservableCollection<FileWrapper>(imageFileNames.Select(fileName => new FileWrapper { FullName = fileName }));
            }
        }

        private void OnFilesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(() => FilesCountText);
        }

        private bool CanSelectFileOrFolder()
        {
            return !BusyHelper.IsBusy;
        }

        private void SelectFiles()
        {
            var dialog = new VistaOpenFileDialog
            {
                Multiselect = true,
                Filter = @"JPEG files (*.jpg)|*.jpg|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                AllFiles = new ObservableCollection<FileWrapper>(dialog.FileNames.Select(s => new FileWrapper { FullName = s }));
            }
        }

        private void SelectFolder()
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                var fileNames = _fileSystemService.GetAllFilesInDirectoryWithExtension(dialog.SelectedPath, ".jpg");
                AllFiles = new ObservableCollection<FileWrapper>(fileNames.Select(fileName => new FileWrapper { FullName = fileName }));
            }
        }

        private void DeleteSelectedFile()
        {
            if ((AllFiles != null) && (SelectedFile != null))
            {
                AllFiles.Remove(SelectedFile);
            }
        }
    }
}