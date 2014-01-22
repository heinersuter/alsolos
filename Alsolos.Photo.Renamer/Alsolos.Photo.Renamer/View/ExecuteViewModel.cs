using System;

namespace Alsolos.Photo.Renamer.View {
    using System.Linq;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.Services;

    public class ExecuteViewModel : ViewModel {
        private readonly FileListViewModel _fileListViewModel;
        private readonly ParameterViewModel _parameterViewModel;
        private readonly FileRenameService _fileRenameController;

        public ExecuteViewModel(FileRenameViewModel fileRenameViewModel) {
            FileRenameViewModel = fileRenameViewModel;
            ConnectIsBusy(fileRenameViewModel);
            _fileListViewModel = fileRenameViewModel.FileListViewModel;
            _parameterViewModel = fileRenameViewModel.ParameterViewModel;
            _fileRenameController = fileRenameViewModel.FileRenameController;
        }

        public FileRenameViewModel FileRenameViewModel { get; private set; }

        public double ExecutionProgress {
            get { return BackingFields.GetValue(() => ExecutionProgress); }
            set { BackingFields.SetValue(() => ExecutionProgress, value); }
        }

        public DelegateCommand ExecuteCommand {
            get { return BackingFields.GetCommand(() => ExecuteCommand, Execute, CanExecute); }
        }

        public DelegateCommand AbortCommand {
            get { return BackingFields.GetCommand(() => AbortCommand, Abort, CanAbort); }
        }

        protected override void OnIsBusyChanged(bool newValue) {
            base.OnIsBusyChanged(newValue);
            ExecuteCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecute() {
            return !IsBusy && _fileListViewModel.AllFiles != null && _fileListViewModel.AllFiles.Any();
        }

        private async void Execute() {
            ExecutionProgress = 0.0;
            IsBusy = true;
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, d) => ExecutionProgress = d;
            await _fileRenameController.RenameFilesAsync(_fileListViewModel.AllFiles, _parameterViewModel.TimeOffset, _parameterViewModel.ConstantName, progress);
            IsBusy = false;
        }

        private bool CanAbort() {
            return IsBusy && !_fileRenameController.DoAbort;
        }

        private void Abort() {
            _fileRenameController.DoAbort = true;
            AbortCommand.RaiseCanExecuteChanged();
        }
    }
}
