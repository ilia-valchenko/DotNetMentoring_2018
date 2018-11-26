using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TaskWindowsServiceModule
{
    public partial class ImagePdfBinderService : ServiceBase
    {
        //private readonly Timer timer;
        private readonly string logName;
        //private readonly Dictionary<string, bool> dictionaryProcessedFiles;
        //private int iteration = 1;
        //private readonly Regex imageFormatRegex;

        public ImagePdfBinderService()
        {
            //timer = new Timer(WorkProcedure);
            logName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["logFileName"]);
            //dictionaryProcessedFiles = new Dictionary<string, bool>();

            //// https://stackoverflow.com/questions/374930/validating-file-types-by-regular-expression
            //imageFormatRegex = new Regex(@"^.*\.(jpg|JPG|gif|GIF|doc|DOC|pdf|PDF)$");
        }

        //private void WorkProcedure(object target)
        //{
        //    File.AppendAllText(logName, DateTime.Now.ToLongTimeString() + $" FileProcessorService iteration {iteration++}.");

        //    try
        //    {
        //        DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["fileStorageFolderName"]));

        //        var files = directoryInfo.GetFiles();

        //        foreach (var file in files)
        //        {
        //            try
        //            {
        //                if(imageFormatRegex.IsMatch(file.Name))
        //                {
        //                    var isProcessedFile = dictionaryProcessedFiles[file.FullName];
        //                }
        //            }
        //            catch (KeyNotFoundException exc)
        //            {
        //                dictionaryProcessedFiles[file.Name] = true;
        //                File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} {file.FullName}");
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
        //    }
        //}

        protected override void OnStart(string[] args)
        {
            //timer.Change(0, 5 * 1000);

            // ---------------------- 2 ----------------------------
            File.AppendAllText(logName, DateTime.Now.ToLongTimeString() + $" FileProcessorService has started. Now it uses FileSystemWatcher.");

            try
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = ConfigurationManager.AppSettings["DirectoryPath"];

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess |
                    NotifyFilters.LastWrite |
                    NotifyFilters.FileName |
                    NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Created += new FileSystemEventHandler(OnCreated);

                // Begin watching.
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception exception)
            {
                File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        protected override void OnStop()
        {
            //timer.Change(Timeout.Infinite, 0);
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} File: {e.FullPath} {e.ChangeType}{Environment.NewLine}");
        }
    }
}
