namespace Alsolos.Photo.Renamer.View {
    using System;
    using System.ComponentModel;
    using System.Windows.Media.Imaging;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.Services;

    public class FilePreviewViewModel : ViewModel {
        private readonly FileListViewModel _fileListViewModel;
        private readonly ParameterViewModel _parameterViewModel;
        private readonly FileRenameService _fileRenameController;
        private readonly FileMetaDataService _fileMetaDataService = new FileMetaDataService();

        public FilePreviewViewModel(FileRenameViewModel fileRenameViewModel) {
            FileRenameViewModel = fileRenameViewModel;
            _fileListViewModel = fileRenameViewModel.FileListViewModel;
            _parameterViewModel = fileRenameViewModel.ParameterViewModel;
            _fileRenameController = fileRenameViewModel.FileRenameController;

            _fileListViewModel.PropertyChanged += OnFileListViewModelPropertyChanged;
            _parameterViewModel.PropertyChanged += OnParameterViewModelPropertyChanged;
            UpdatePreviewFileName();
        }

        public FileRenameViewModel FileRenameViewModel { get; private set; }

        public string PreviewFileName {
            get { return BackingFields.GetValue(() => PreviewFileName); }
            private set { BackingFields.SetValue(() => PreviewFileName, value); }
        }

        public BitmapImage BitmapImage {
            get { return BackingFields.GetValue(() => BitmapImage); }
            private set { BackingFields.SetValue(() => BitmapImage, value); }
        }

        public DateTime? CreatedTime {
            get { return BackingFields.GetValue(() => CreatedTime); }
            private set { BackingFields.SetValue(() => CreatedTime, value); }
        }

        private void OnFileListViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == GetPropertyName(() => _fileListViewModel.SelectedFile)) {
                UpdatePreviewFileName();
                UpdatePreviewImage();
            }
        }

        private void OnParameterViewModelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            UpdatePreviewFileName();
        }

        private void UpdatePreviewFileName() {
            if (_fileListViewModel.SelectedFile != null) {
                if (_fileListViewModel.SelectedFile.CreatedTime == null) {
                    _fileListViewModel.SelectedFile.CreatedTime = _fileMetaDataService.GetExifTime(_fileListViewModel.SelectedFile.FullName);
                }
                CreatedTime = _fileListViewModel.SelectedFile.CreatedTime;
            } else {
                CreatedTime = null;
            }
            var fileCount = _fileListViewModel.AllFiles != null ? _fileListViewModel.AllFiles.Count : 0;
            PreviewFileName = _fileRenameController.CalculateNewFileName(CreatedTime, 0, fileCount, _parameterViewModel.TimeOffset, _parameterViewModel.ConstantName);
        }

        private void UpdatePreviewImage() {
            if (_fileListViewModel.SelectedFile != null) {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(_fileListViewModel.SelectedFile.FullName);
                image.EndInit();
                BitmapImage = image;
            } else {
                BitmapImage = null;
            }
        }
    }
}
