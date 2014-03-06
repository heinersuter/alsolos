namespace Alsolos.Photo.Renamer.Services {
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Alsolos.Photo.Renamer.Model;

    public class FileRenameService {
        private readonly FileSystemService _fileSystemService;
        private readonly FileMetaDataService _fileMetaDataService;

        public FileRenameService() {
            _fileSystemService = new FileSystemService();
            _fileMetaDataService = new FileMetaDataService();
        }

        public bool DoAbort { get; set; }

        public void RenameFiles(IList<FileWrapper> files, TimeSpan timeOffset, string constantName, IProgress<double> progress) {
            DoAbort = false;

            for (var i = 0; i < files.Count && !DoAbort; i++) {
                var file = files[i];
                var directory = Path.GetDirectoryName(file.FullName);
                if (file.CreatedTime == null) {
                    file.CreatedTime = _fileMetaDataService.GetExifTime(file.FullName);
                }
                var newFileName = CalculateNewFileName(file.CreatedTime, i, files.Count, timeOffset, constantName);
                var newFullName = Path.Combine(directory ?? string.Empty, newFileName);
                _fileSystemService.RenameFile(file, newFullName, files);
                if (progress != null) {
                    progress.Report((i + 1.0) / files.Count);
                }
            }
        }

        public Task RenameFilesAsync(IList<FileWrapper> files, TimeSpan timeOffset, string constantName, IProgress<double> progress) {
            return Task.Run(() => RenameFiles(files, timeOffset, constantName, progress));
        }

        public string CalculateNewFileName(DateTime? createdTime, int index, int count, TimeSpan timeOffset, string constantName) {
            var newTime = createdTime + timeOffset ?? DateTime.Today + timeOffset;
            return newTime.ToString("yyyyMMdd_HHmmss_") + constantName + FormatIndex(index, count) + ".jpg";
        }

        private string FormatIndex(int index, int count) {
            return (index + 1).ToString(CultureInfo.InvariantCulture).PadLeft(count.ToString(CultureInfo.InvariantCulture).Length, '0');
        }
    }
}