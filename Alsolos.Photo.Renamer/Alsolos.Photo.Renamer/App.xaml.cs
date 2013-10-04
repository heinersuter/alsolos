namespace Alsolos.Photo.Renamer
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Markup;
    using Alsolos.Photo.Renamer.Properties;

    public partial class App
    {
        private void OnApplicationStartup(object sender, System.Windows.StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
            }
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
