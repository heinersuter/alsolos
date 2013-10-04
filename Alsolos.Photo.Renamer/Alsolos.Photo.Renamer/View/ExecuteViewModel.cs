using System;

namespace Alsolos.Photo.Renamer.View {
    using System.Linq;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.Services;

    public class ExecuteViewModel : ViewModel {
        private readonly FilesSelectorViewModel _filesSelectorViewModel;
        private readonly ParameterViewModel _parameterViewModel;
        private readonly FileRenameService _fileRenameController;

        public ExecuteViewModel(FileRenameViewModel fileRenameViewModel) {
            FileRenameViewModel = fileRenameViewModel;
            ConnectIsBusy(fileRenameViewModel);
            _filesSelectorViewModel = fileRenameViewModel.FilesSelectorViewModel;
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
            return !IsBusy && _filesSelectorViewModel.AllFiles != null && _filesSelectorViewModel.AllFiles.Any();
        }

        private async void Execute() {
            ExecutionProgress = 0.0;
            IsBusy = true;
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, d) => ExecutionProgress = d;
            await _fileRenameController.RenameFilesAsync(_filesSelectorViewModel.AllFiles, _parameterViewModel.TimeOffset, _parameterViewModel.ConstantName, progress);
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
