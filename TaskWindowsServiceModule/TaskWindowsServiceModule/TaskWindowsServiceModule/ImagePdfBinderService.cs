using System;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TaskWindowsServiceModule
{
    public partial class ImagePdfBinderService : ServiceBase
    {
        private readonly string logName;
        private int _counter = 0;
        private Document _doc;

        public ImagePdfBinderService()
        {
            logName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["logFileName"]);
            _doc = new Document();
            PdfWriter.GetInstance(_doc, new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["pdfFileName"]), FileMode.Create));
            _doc.Open();
            _doc.Add(new Paragraph("Hello World!"));
        }

        protected override void OnStart(string[] args)
        {
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
                watcher.Filter = "*.jpg";

                // TODO: I stoped here.
                //watcher.WaitForChanged

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
            File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} ON STOP");

            try
            {
                _doc.Close();
            }
            catch (Exception exception)
            {
                File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} File: {e.FullPath} {e.ChangeType}{Environment.NewLine}");

            try
            {
                // The problem is that the FileSystemWatcher tells you immediately when
                // the file was created. It doesn't wait for the file to be released.
                // The system is still creating the file when you already have the message,
                // which is why you are getting that error message. Basically, you need to
                // wait your turn. Take the event and then have a utility watch that file
                // until it is free for reading.

                //while (WaitForFile(e.FullPath) == false) ;
                Thread.Sleep(1000);

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(e.FullPath);
                jpg.Alignment = Element.ALIGN_CENTER;
                _doc.Add(jpg);
            }
            catch (Exception exception)
            {
                File.AppendAllText(logName, $"{Environment.NewLine}{DateTime.Now.ToLongTimeString()} Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
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
                    using (FileStream fs = new FileStream(fullPath,
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
                    File.AppendAllText(logName,
                       $"WaitForFile {fullPath} failed to get an exclusive lock: {ex.ToString()}");

                    if (numTries > 10)
                    {
                        File.AppendAllText(logName,
                            $"WaitForFile {fullPath} giving up after 10 tries");
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            File.AppendAllText(logName, $"WaitForFile {fullPath} returning true after {numTries} tries");
            return true;
        }
    }
}
