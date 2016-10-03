using System.Reflection;
using Alsolos.Commons.Wpf.Mvvm;
using Alsolos.Photo.Renamer.View;

namespace Alsolos.Photo.Renamer
{
    public class MainViewModel : ViewModel
    {
        public string Title
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                var version = assembly.GetName().Version;
                return $"Alsolos Photo Renamer {version.Major}.{version.Minor}";
            }
        }

        public FileRenameViewModel FileRenameViewModel
        {
            get { return BackingFields.GetValue(() => new FileRenameViewModel()); }
        }
    }
}