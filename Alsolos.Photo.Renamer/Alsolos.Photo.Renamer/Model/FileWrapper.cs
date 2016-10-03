using System;
using System.IO;
using Alsolos.Commons.Wpf.Mvvm;

namespace Alsolos.Photo.Renamer.Model
{
    public class FileWrapper : BackingFieldsHolder
    {
        public string FullName
        {
            get { return BackingFields.GetValue(() => FullName); }
            set
            {
                if (BackingFields.SetValue(value))
                {
                    Name = !string.IsNullOrWhiteSpace(FullName) ? Path.GetFileName(FullName) : string.Empty;
                }
            }
        }

        public DateTime? CreatedTime
        {
            get { return BackingFields.GetValue<DateTime?>(); }
            set { BackingFields.SetValue(value); }
        }

        public string Name
        {
            get { return BackingFields.GetValue<string>(); }
            private set { BackingFields.SetValue(value); }
        }
    }
}