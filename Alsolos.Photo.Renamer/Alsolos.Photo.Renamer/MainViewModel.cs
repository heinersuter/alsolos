namespace Alsolos.Photo.Renamer {
    using System.Reflection;
    using Alsolos.Commons.Mvvm;
    using Alsolos.Photo.Renamer.View;

    public class MainViewModel : ViewModel {
        public string Title {
            get {
                var assembly = Assembly.GetEntryAssembly();
                var version = assembly.GetName().Version;
                return string.Format("Alsolos Photo Renamer {0}.{1}", version.Major, version.Minor);
            }
        }

        public FileRenameViewModel FileRenameViewModel {
            get { return BackingFields.GetValue(() => FileRenameViewModel, () => new FileRenameViewModel()); }
        }
    }
}
