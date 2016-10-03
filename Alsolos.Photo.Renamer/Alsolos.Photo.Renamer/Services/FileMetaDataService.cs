using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Alsolos.Photo.Renamer.Services
{
    public class FileMetaDataService
    {
        public DateTime GetExifTime(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var bitmap = BitmapFrame.Create(stream);
                var metadata = (BitmapMetadata)bitmap.Metadata;
                var date = metadata?.DateTaken;
                return date != null ? DateTime.Parse(date) : new DateTime();
            }
        }
    }
}