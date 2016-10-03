using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Alsolos.Photo.Renamer.Model;

namespace Alsolos.Photo.Renamer.Services
{
    public class FileRenameService
    {
        private readonly FileMetaDataService _fileMetaDataService;
        private readonly FileSystemService _fileSystemService;

        public FileRenameService()
        {
            _fileSystemService = new FileSystemService();
            _fileMetaDataService = new FileMetaDataService();
        }

        public bool DoAbort { get; set; }

        public void RenameFiles(IList<FileWrapper> files, TimeSpan timeOffset, string constantName, IProgress<double> progress)
        {
            DoAbort = false;

            for (var i = 0; (i < files.Count) && !DoAbort; i++)
            {
                var file = files[i];
                var directory = Path.GetDirectoryName(file.FullName);
                if (file.CreatedTime == null)
                {
                    file.CreatedTime = _fileMetaDataService.GetExifTime(file.FullName);
                }
                var newFileName = CalculateNewFileName(file.CreatedTime, i, files.Count, timeOffset, constantName);
                var newFullName = Path.Combine(directory ?? string.Empty, newFileName);
                _fileSystemService.RenameFile(file, newFullName, files);
                progress?.Report((i + 1.0) / files.Count);
            }
        }

        public Task RenameFilesAsync(IList<FileWrapper> files, TimeSpan timeOffset, string constantName, IProgress<double> progress)
        {
            return Task.Run(() => RenameFiles(files, timeOffset, constantName, progress));
        }

        public DateTime CalculateAdjustedTime(DateTime? createdTime, TimeSpan timeOffset)
        {
            return createdTime + timeOffset ?? DateTime.Today + timeOffset;
        }

        public string CalculateNewFileName(DateTime? createdTime, int index, int count, TimeSpan timeOffset, string constantName)
        {
            var newTime = CalculateAdjustedTime(createdTime, timeOffset);
            return newTime.ToString("yyyyMMdd_HHmmss_") + constantName + FormatIndex(index, count) + ".jpg";
        }

        private string FormatIndex(int index, int count)
        {
            return (index + 1).ToString(CultureInfo.InvariantCulture).PadLeft(count.ToString(CultureInfo.InvariantCulture).Length, '0');
        }
    }
}