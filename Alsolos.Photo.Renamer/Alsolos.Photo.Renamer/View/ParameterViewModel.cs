﻿namespace Alsolos.Photo.Renamer.View {
    using System;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.Properties;

    public class ParameterViewModel : ViewModel {
        public ParameterViewModel(FileRenameViewModel fileRenameViewModel) {
            FileRenameViewModel = fileRenameViewModel;
        }

        public FileRenameViewModel FileRenameViewModel { get; private set; }

        public string ConstantName {
            get { return BackingFields.GetValue(() => ConstantName, () => Settings.Default.ConstantName); }
            set { BackingFields.SetValue(() => ConstantName, value, s => Settings.Default.ConstantName = ConstantName); }
        }

        public TimeSpan TimeOffset {
            get { return BackingFields.GetValue(() => TimeOffset, () => Settings.Default.TimeOffset); }
            set {
                if (BackingFields.SetValue(() => TimeOffset, value)) {
                    Settings.Default.TimeOffset = TimeOffset;
                    RaisePropertyChanged(() => Days);
                    RaisePropertyChanged(() => Hours);
                    RaisePropertyChanged(() => Minutes);
                    RaisePropertyChanged(() => Seconds);
                }
            }
        }

        public int Days {
            get { return TimeOffset.Days; }
            set { TimeOffset = new TimeSpan(value, Hours, Minutes, Seconds); }
        }

        public int Hours {
            get { return TimeOffset.Hours; }
            set { TimeOffset = new TimeSpan(Days, value, Minutes, Seconds); }
        }

        public int Minutes {
            get { return TimeOffset.Minutes; }
            set { TimeOffset = new TimeSpan(Days, Hours, value, Seconds); }
        }

        public int Seconds {
            get { return TimeOffset.Seconds; }
            set { TimeOffset = new TimeSpan(Days, Hours, Minutes, value); }
        }

        public DelegateCommand ResetCommand {
            get { return BackingFields.GetCommand(() => ResetCommand, Reset, CanReset); }
        }

        private bool CanReset() {
            return TimeOffset != TimeSpan.Zero || !string.IsNullOrEmpty(ConstantName);
        }

        private void Reset() {
            TimeOffset = TimeSpan.Zero;
            ConstantName = string.Empty;
        }
    }
}
