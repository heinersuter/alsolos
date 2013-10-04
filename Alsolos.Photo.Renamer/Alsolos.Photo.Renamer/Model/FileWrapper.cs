namespace Alsolos.Photo.Renamer.Model {
    using System;
    using System.IO;
    using Alsolos.Commons.Mvvm;

    public class FileWrapper : BackingFieldsHolder {
        public string FullName {
            get { return BackingFields.GetValue(() => FullName); }
            set {
                if (BackingFields.SetValue(() => FullName, value)) {
                    Name = !string.IsNullOrWhiteSpace(FullName) ? Path.GetFileName(FullName) : string.Empty;
                }
            }
        }

        public DateTime? CreatedTime {
            get { return BackingFields.GetValue(() => CreatedTime); }
            set { BackingFields.SetValue(() => CreatedTime, value); }
        }

        public string Name {
            get { return BackingFields.GetValue(() => Name); }
            private set { BackingFields.SetValue(() => Name, value); }
        }
    }
}
