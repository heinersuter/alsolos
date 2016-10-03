using System;
using System.Linq;
using Alsolos.Commons.Wpf.Controls.Progress;
using Alsolos.Commons.Wpf.Mvvm;
using Alsolos.Photo.Renamer.Services;

namespace Alsolos.Photo.Renamer.View
{
    public class ExecuteViewModel : BusyViewModel
    {
        private readonly FileListViewModel _fileListViewModel;
        private readonly FileRenameService _fileRenameController;
        private readonly ParameterViewModel _parameterViewModel;

        public ExecuteViewModel(FileRenameViewModel fileRenameViewModel)
        {
            FileRenameViewModel = fileRenameViewModel;
            //ConnectIsBusy(fileRenameViewModel);
            _fileListViewModel = fileRenameViewModel.FileListViewModel;
            _parameterViewModel = fileRenameViewModel.ParameterViewModel;
            _fileRenameController = fileRenameViewModel.FileRenameController;
        }

        public FileRenameViewModel FileRenameViewModel { get; private set; }

        public double ExecutionProgress
        {
            get { return BackingFields.GetValue<double>(); }
            set { BackingFields.SetValue(value); }
        }

        public DelegateCommand ExecuteCommand => BackingFields.GetCommand(Execute, CanExecute);

        public DelegateCommand AbortCommand => BackingFields.GetCommand(Abort, CanAbort);

        //protected override void OnIsBusyChanged(bool newValue)
        //{
        //    base.OnIsBusyChanged(newValue);
        //    ExecuteCommand.RaiseCanExecuteChanged();
        //}

        private bool CanExecute()
        {
            return !BusyHelper.IsBusy && (_fileListViewModel.AllFiles != null) && _fileListViewModel.AllFiles.Any();
        }

        private async void Execute()
        {
            ExecutionProgress = 0.0;
            BusyHelper.IsBusy = true;
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, d) => ExecutionProgress = d;
            await _fileRenameController.RenameFilesAsync(_fileListViewModel.AllFiles, _parameterViewModel.TimeOffset, _parameterViewModel.ConstantName, progress);
            BusyHelper.IsBusy = false;
        }

        private bool CanAbort()
        {
            return BusyHelper.IsBusy && !_fileRenameController.DoAbort;
        }

        private void Abort()
        {
            _fileRenameController.DoAbort = true;
            AbortCommand.RaiseCanExecuteChanged();
        }
    }
}