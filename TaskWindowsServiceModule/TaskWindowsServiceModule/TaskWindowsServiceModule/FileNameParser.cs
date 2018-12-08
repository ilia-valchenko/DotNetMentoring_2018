using System;
using System.Linq;

namespace TaskWindowsServiceModule
{
    /// <summary>
    /// The file name parser.
    /// </summary>
    public static class FileNameParser
    {
        /// <summary>
        /// Extract number from the name of the file.
        /// </summary>
        /// <param name="filename">The full name of the file. Extension is also included.</param>
        /// <returns>
        /// Returns the number of the file.
        /// </returns>
        public static int ExtractNumberFromFileName(string filename)
        {
            if(string.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"{nameof(filename)} is null or empty.");
            }

            string[] splittedFileName = filename.Split('_');
            string fileNumber = splittedFileName.Last().Split('.').First();

            return Int32.Parse(fileNumber);
        }
    }
}
