using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alsolos.Photo.Renamer.Model;

namespace Alsolos.Photo.Renamer.Services
{
    public class FileSystemService
    {
        public void RenameFile(FileWrapper oldFile, string newFileName, IEnumerable<FileWrapper> allKnownFiles)
        {
            if (oldFile.FullName == newFileName)
            {
                return;
            }

            if (File.Exists(newFileName))
            {
                var tempFileName = CalculateTempFileName(newFileName);
                File.Move(newFileName, tempFileName);

                var knownFile = allKnownFiles.FirstOrDefault(model => model.FullName == newFileName);
                if (knownFile != null)
                {
                    knownFile.FullName = tempFileName;
                }
            }
            File.Move(oldFile.FullName, newFileName);
            oldFile.FullName = newFileName;
        }

        public IEnumerable<string> GetAllFilesInDirectoryWithExtension(string directory, string extension)
        {
            return Directory.GetFiles(directory).Where(fileName => fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase));
        }

        private static string CalculateTempFileName(string fileName)
        {
            var i = 0;
            var direcotry = Path.GetDirectoryName(fileName) ?? string.Empty;
            var extension = Path.GetExtension(fileName);
            while (File.Exists(fileName))
            {
                fileName = Path.Combine(direcotry, Path.GetFileNameWithoutExtension(fileName) + $"_temp_{i}" + extension);
                i++;
            }
            return fileName;
        }
    }
}