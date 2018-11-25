using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;

namespace TaskWindowsServiceModule
{
    public partial class ImagePdfBinderService : ServiceBase
    {
        private readonly Timer timer;
        private readonly string logName;
        private readonly Dictionary<string, bool> dictionaryProcessedFiles;
        private int iteration = 1;

        public ImagePdfBinderService()
        {
            timer = new Timer(WorkProcedure);
            logName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["logFileName"]);
            dictionaryProcessedFiles = new Dictionary<string, bool>();
        }

        private void WorkProcedure(object target)
        {
            File.AppendAllText(logName, DateTime.Now.ToLongTimeString() + $" FileProcessorService iteration {iteration++}.");

            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["fileStorageFolderName"]));

                var files = directoryInfo.GetFiles();

                foreach (var file in files)
                {
                    try
                    {
                        var isProcessedFile = dictionaryProcessedFiles[file.FullName];
                    }
                    catch (KeyNotFoundException exc)
                    {
                        dictionaryProcessedFiles[file.Name] = true;
                        File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} {file.FullName}");
                    }
                }
            }
            catch (Exception exception)
            {
                File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Change(0, 5 * 1000);
        }

        protected override void OnStop()
        {
            timer.Change(Timeout.Infinite, 0);
        }
    }
}
