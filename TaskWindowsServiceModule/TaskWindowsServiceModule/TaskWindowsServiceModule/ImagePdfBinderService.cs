using System;
using System.IO;
using System.ServiceProcess;
using System.Configuration;
using iTextSharp.text;
using TaskWindowsServiceModule.Extensions;

namespace TaskWindowsServiceModule
{
    /// <summary>
    /// The PDF image binder windows service.
    /// </summary>
    public partial class ImagePdfBinderService : ServiceBase
    {
        private readonly PdfDocumentService _pdfDocumentService;
        private readonly FileWatcherHelper _fileWatcherHelper;
        private readonly SimpleLogger _logger;
        private readonly string _imageFileExtension;
        private int _numberOfLastImage = 0;
        private Document _doc;
        private bool isFirstFile = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePdfBinderService"/> class.
        /// </summary>
        public ImagePdfBinderService()
        {
            _pdfDocumentService = new PdfDocumentService();
            _fileWatcherHelper = new FileWatcherHelper();
            _logger = new SimpleLogger();
            _imageFileExtension = ConfigurationManager.AppSettings["imageFileExtension"];
            _doc = _pdfDocumentService.CreateNextPdfDocument();
            _doc.Open();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Log("Service started.");

            try
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    ConfigurationManager.AppSettings["DirectoryName"]);

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess |
                    NotifyFilters.LastWrite |
                    NotifyFilters.FileName |
                    NotifyFilters.DirectoryName;

                watcher.Filter = "*" + _imageFileExtension;
                watcher.Created += new FileSystemEventHandler(OnCreated);

                // Begin watching.
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception exception)
            {
                _logger.Log($"Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        protected override void OnStop()
        {
            _logger.Log("Service is stopping.");

            try
            {
                _doc.Close();
            }
            catch (Exception exception)
            {
                _logger.Log($"Error has occured during closing PDF document. Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }

            _logger.Log("PDF document was closed.");
            _logger.Log("Service stopped.");
        }

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            _logger.Log($"File: {e.FullPath} {e.ChangeType}{Environment.NewLine}");

            try
            {
                // The problem is that the FileSystemWatcher tells you immediately when
                // the file was created. It doesn't wait for the file to be released.
                // The system is still creating the file when you already have the message,
                // which is why you are getting that error message. Basically, you need to
                // wait your turn. Take the event and then have a utility watch that file
                // until it is free for reading.
                _fileWatcherHelper.WaitUntilFileIsReleased(e.FullPath);

                int numberOfCurrentImage = FileNameParser.ExtractNumberFromFileName(e.Name);

                _logger.Log($"The number of the current file is {numberOfCurrentImage}.");

                if (isFirstFile)
                {
                    isFirstFile = false;
                }
                else if(_numberOfLastImage + 1 != numberOfCurrentImage)
                {
                    _doc.Close();
                    _doc = _pdfDocumentService.CreateNextPdfDocument();
                    _doc.Open();
                }

                _doc.AddImage(Image.GetInstance(e.FullPath));
                _numberOfLastImage = numberOfCurrentImage;
            }
            catch (IOException ioException)
            {
                _logger.Log($"Error message: {ioException.Message}{Environment.NewLine}StackTrace: {ioException.StackTrace}");
                _logger.Log($"The file {e.Name} will be moved to a separate folder.");

                _fileWatcherHelper.MoveFileToFolder(
                    e.Name,
                    e.FullPath,
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        ConfigurationManager.AppSettings["notLoadedImagesFolderName"]));
            }
            catch (Exception exception)
            {
                _logger.Log($"Error has occured during adding image to a PDF document. Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }
    }
}
