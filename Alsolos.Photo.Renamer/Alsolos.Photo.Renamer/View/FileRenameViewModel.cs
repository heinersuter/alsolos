﻿using Alsolos.Photo.Renamer.Services;

namespace Alsolos.Photo.Renamer.View {
    using Alsolos.Commons.Mvvm;

    public class FileRenameViewModel : ViewModel {
        public FileRenameService FileRenameController {
            get { return BackingFields.GetValue(() => FileRenameController, () => new FileRenameService()); }
        }

        public FileListViewModel FileListViewModel {
            get { return BackingFields.GetValue(() => FileListViewModel, () => new FileListViewModel(this)); }
            set { BackingFields.SetValue(() => FileListViewModel, value); }
        }

        public ParameterViewModel ParameterViewModel {
            get { return BackingFields.GetValue(() => ParameterViewModel, () => new ParameterViewModel(this)); }
            set { BackingFields.SetValue(() => ParameterViewModel, value); }
        }

        public FilePreviewViewModel FilePreviewViewModel {
            get { return BackingFields.GetValue(() => FilePreviewViewModel, () => new FilePreviewViewModel(this)); }
            set { BackingFields.SetValue(() => FilePreviewViewModel, value); }
        }

        public ExecuteViewModel ExecuteViewModel {
            get { return BackingFields.GetValue(() => ExecuteViewModel, () => new ExecuteViewModel(this)); }
            set { BackingFields.SetValue(() => ExecuteViewModel, value); }
        }
    }
}