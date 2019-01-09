using log4net;
using log4net.Config;
using System;

namespace MessageQueueTask.Logger
{
    /// <summary>
    /// The log4net wrapper.
    /// </summary>
    public class Logger : ILogger
    {
        private readonly ILog _log;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            XmlConfigurator.Configure();
            _log = LogManager.GetLogger("SimpleLogger");
        }

        public void Error(Exception exception)
        {
            if(exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            _log.Error(exception);
        }

        public void Error(string message)
        {
            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentException($"{nameof(message)} is null or empty.");
            }

            _log.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            if(exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentException($"{nameof(message)} is null or empty.");
            }
        }

        public void Info(string message)
        {
            if(string.IsNullOrEmpty(message))
            {
                throw new ArgumentException($"{nameof(message)} is null or empty.");
            }

            _log.Info(message);
        }
    }
}
