using MessageQueueTask.Logger;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace MessageQueueTask.FileService
{
    public class FileService : IFileService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        public FileService(ILogger logger)
        {
            _logger = logger;
        }

        public void MoveFileToFolder(string fileName, string filePath, string folderPath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path is null or empty.");
            }

            if (string.IsNullOrEmpty(folderPath))
            {
                throw new ArgumentException("The path of the destination folder is null or empty.");
            }

            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.Move(filePath, Path.Combine(folderPath, fileName));
            }
            catch (Exception exc)
            {
                _logger.Error($"Image has not been moved to the {folderPath}");
                _logger.Error($"Error message: {exc.Message}{Environment.NewLine}StackTrace: {exc.StackTrace}");
            }
        }

        public void WaitUntilFileIsReleased(string fullPath)
        {
            while (WaitForFile(fullPath) == false);
        }

        public int ExtractNumberFromFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"{nameof(filename)} is null or empty.");
            }

            string[] splittedFileName = filename.Split('_');
            string fileNumber = splittedFileName.Last().Split('.').First();

            return Int32.Parse(fileNumber);
        }

        private bool WaitForFile(string fullPath)
        {
            int numTries = 0;

            while (true)
            {
                ++numTries;

                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(
                        fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (numTries > 10)
                    {
                        _logger.Error($"WaitForFile {fullPath} giving up after 10 tries");
                        return false;
                    }

                    // Wait for the lock to be released
                    Thread.Sleep(500);
                }
            }

            _logger.Info($"WaitForFile {fullPath} returning true after {numTries} tries");
            return true;
        }
    }
}
