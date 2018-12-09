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
            var logFileName = ConfigurationManager.AppSettings["logFileName"]
                 + "_"
                 + DateTime.Now.ToLongTimeString().Replace(':', '-')
                 + ".log";

            logName = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                logFileName);
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
