using System;
using System.IO;
using System.Threading;

namespace TaskWindowsServiceModule
{
    /// <summary>
    /// The file watcher helper class. Provides some extra logic for working
    /// with files.
    /// </summary>
    public class FileWatcherHelper
    {
        private readonly SimpleLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWatcherHelper"/> class.
        /// </summary>
        public FileWatcherHelper()
        {
            _logger = new SimpleLogger();
        }

        /// <summary>
        /// Waits until files is released.
        /// </summary>
        /// <param name="fullPath">The full path of the file.</param>
        public void WaitUntilFileIsReleased(string fullPath)
        {
            while (WaitForFile(fullPath) == false) ;
        }

        /// <summary>
        /// Moves file to a folder.
        /// </summary>
        /// <param name="fileName">The name of the file that have to be moved.</param>
        /// <param name="filePath">The full path of the file.</param>
        /// <param name="folderPath">The full path of the destination foler.</param>
        public void MoveFileToFolder(string fileName, string filePath, string folderPath)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path is null or empty.");
            }

            if(string.IsNullOrEmpty(folderPath))
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
            catch(Exception exc)
            {
                _logger.Log($"Image has not been moved to the {folderPath}");
                _logger.Log($"Error message: {exc.Message}{Environment.NewLine}StackTrace: {exc.StackTrace}");
            }
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
                    _logger.Log($"WaitForFile {fullPath} failed to get an exclusive lock: {ex.ToString()}");

                    if (numTries > 10)
                    {
                        _logger.Log($"WaitForFile {fullPath} giving up after 10 tries");
                        return false;
                    }

                    // Wait for the lock to be released
                    Thread.Sleep(500);
                }
            }

            _logger.Log($"WaitForFile {fullPath} returning true after {numTries} tries");
            return true;
        }
    }
}
