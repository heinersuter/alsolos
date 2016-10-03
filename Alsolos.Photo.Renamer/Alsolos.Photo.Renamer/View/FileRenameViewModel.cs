using Alsolos.Commons.Wpf.Controls.Progress;
using Alsolos.Photo.Renamer.Services;

namespace Alsolos.Photo.Renamer.View
{
    public class FileRenameViewModel : BusyViewModel
    {
        public FileRenameService FileRenameController
        {
            get { return BackingFields.GetValue(() => new FileRenameService()); }
        }

        public FileListViewModel FileListViewModel
        {
            get { return BackingFields.GetValue(() => new FileListViewModel(this)); }
            set { BackingFields.SetValue(value); }
        }

        public ParameterViewModel ParameterViewModel
        {
            get { return BackingFields.GetValue(() => new ParameterViewModel(this)); }
            set { BackingFields.SetValue(value); }
        }

        public FilePreviewViewModel FilePreviewViewModel
        {
            get { return BackingFields.GetValue(() => new FilePreviewViewModel(this)); }
            set { BackingFields.SetValue(value); }
        }

        public ExecuteViewModel ExecuteViewModel
        {
            get { return BackingFields.GetValue(() => new ExecuteViewModel(this)); }
            set { BackingFields.SetValue(value); }
        }
    }
}