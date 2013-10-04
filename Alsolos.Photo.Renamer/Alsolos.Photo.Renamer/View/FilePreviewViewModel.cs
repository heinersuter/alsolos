namespace Alsolos.Photo.Renamer.View {
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Media.Imaging;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.Services;

    public class FilePreviewViewModel : ViewModel {
        private readonly FilesSelectorViewModel _filesSelectorViewModel;
        private readonly ParameterViewModel _parameterViewModel;
        private readonly FileRenameService _fileRenameController;

        public FilePreviewViewModel(FileRenameViewModel fileRenameViewModel) {
            FileRenameViewModel = fileRenameViewModel;
            _filesSelectorViewModel = fileRenameViewModel.FilesSelectorViewModel;
            _parameterViewModel = fileRenameViewModel.ParameterViewModel;
            _fileRenameController = fileRenameViewModel.FileRenameController;

            _filesSelectorViewModel.PropertyChanged += OnFileSelectorPropertyChanged;
            _parameterViewModel.PropertyChanged += OnParameterPropertyChanged;
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

        private void OnFileSelectorPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == GetPropertyName(() => _filesSelectorViewModel.SelectedFile)) {
                UpdatePreviewFileName();
                UpdatePreviewImage();
            }
        }

        private void OnParameterPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            UpdatePreviewFileName();
        }

        private void UpdatePreviewFileName() {
            CreatedTime = _filesSelectorViewModel.SelectedFile != null ? _filesSelectorViewModel.SelectedFile.CreatedTime : null;
            var fileCount = _filesSelectorViewModel.AllFiles != null ? _filesSelectorViewModel.AllFiles.Count : 0;
            PreviewFileName = _fileRenameController.CalculateNewFileName(CreatedTime, 0, fileCount, _parameterViewModel.TimeOffset, _parameterViewModel.ConstantName);
        }

        private void UpdatePreviewImage() {
            if (_filesSelectorViewModel.SelectedFile != null) {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(_filesSelectorViewModel.SelectedFile.FullName);
                image.EndInit();
                BitmapImage = image;
            } else {
                BitmapImage = null;
            }
        }
    }
}
