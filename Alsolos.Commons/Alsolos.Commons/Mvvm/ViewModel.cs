namespace Alsolos.Commons.Mvvm {
    using Alsolos.Commons.Utils;

    public abstract class ViewModel : BackingFieldsHolder {
        private ViewModel _parentViewModel;

        public event ValueEventHandler<bool> IsBusyChanged;

        public bool IsBusy {
            get { return BackingFields.GetValue(() => IsBusy); }
            set { BackingFields.SetValue(() => IsBusy, value, OnIsBusyChanged); }
        }

        protected virtual void OnIsBusyChanged(bool isBusy) {
            var handler = IsBusyChanged;
            if (handler != null) {
                handler(this, new ValueEventArgs<bool>(isBusy));
            }
            if (_parentViewModel != null) {
                _parentViewModel.IsBusy = isBusy;
            }
        }

        protected void ConnectIsBusy(ViewModel parentViewModel) {
            if (_parentViewModel != null) {
                _parentViewModel.IsBusyChanged -= OnParentIsBusyChanged;
            }
            _parentViewModel = parentViewModel;
            if (_parentViewModel != null) {
                parentViewModel.IsBusyChanged += OnParentIsBusyChanged;
            }
        }

        private void OnParentIsBusyChanged(object source, ValueEventArgs<bool> args) {
            IsBusy = args.Value;
        }
    }
}
