namespace Alsolos.Photo.Renamer.Services {
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    public class FileMetaDataService {
        public DateTime GetExifTime(string fileName) {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                var bitmap = BitmapFrame.Create(stream);
                var metadata = (BitmapMetadata)bitmap.Metadata;
                var date = metadata != null ? metadata.DateTaken : null;
                if (date != null) {
                    return DateTime.Parse(date);
                }
                return new DateTime();
            }
        }
    }
}
