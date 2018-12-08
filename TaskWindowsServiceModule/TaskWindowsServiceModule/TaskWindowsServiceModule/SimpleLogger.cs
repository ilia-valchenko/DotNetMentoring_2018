using System;
using System.Configuration;
using System.IO;

namespace TaskWindowsServiceModule
{
    /// <summary>
    /// The simple logger.
    /// </summary>
    public class SimpleLogger
    {
        private readonly string logName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLogger"/> class.
        /// </summary>
        public SimpleLogger()
        {
            logName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["logFileName"]);
        }

        /// <summary>
        /// Writes a message in a log file.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            File.AppendAllText(logName, $"{DateTime.Now.ToLongTimeString()} - {message}{Environment.NewLine}");
        }
    }
}
