namespace AttendanceRecorder.View.About
{
    using System.Reflection;
    using System.Windows;

    public partial class AboutDialog
    {
        static AboutDialog()
        {
            var assembly = Assembly.GetEntryAssembly();

            Version = assembly.GetName().Version.ToString();
            
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length == 1)
            {
                ApplicationName = ((AssemblyTitleAttribute)attributes[0]).Title;
            }

            attributes = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 1)
            {
                Author = ((AssemblyCompanyAttribute)attributes[0]).Company;
            }

            attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 1)
            {
                Description = ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }

            attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 1)
            {
                Copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public AboutDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static string ApplicationName { get; private set; }

        public static string Version { get; private set; }

        public static string Description { get; private set; }

        public static string Author { get; private set; }

        public static string Copyright { get; private set; }
    }
}
