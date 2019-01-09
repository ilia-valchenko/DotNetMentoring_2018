using CenterQueueClient;
using iTextSharp.text;
using MessageQueueTask.FileService;
using MessageQueueTask.Logger;
using MessageQueueTask.PdfDocumentService;
using MessageQueueTask.PdfDocumentService.Extensions;
using System;
using System.Configuration;
using System.IO;

namespace MessageQueueTask.ImageWatcherService
{
    public class ImageService : IImageService, IDisposable
    {
        private readonly IFileService _fileService;
        private readonly IPdfDocumentService _pdfDocumentService;
        private readonly ILogger _logger;
        private readonly ICenterQueueClient _centerQueueClient;
        private readonly string _imageFileExtension;
        private int _numberOfLastImage = 0;
        private Document _doc;
        private bool _isFirstFile = true;
        private bool _isDisposed = false;

        // test
        private MemoryStream _memoryStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        public ImageService(ILogger logger)
        {
            _logger = logger;
            _fileService = new FileService.FileService(logger);
            _pdfDocumentService = new PdfDocumentService.PdfDocumentService(logger);
            string centerQueueName = ConfigurationManager.AppSettings["centerQueueName"];
            _centerQueueClient = new CenterQueueClient<Document>(centerQueueName, logger);
            _imageFileExtension = ConfigurationManager.AppSettings["imageFileExtension"];
            _doc = _pdfDocumentService.CreateNextPdfDocument(ref _memoryStream);
            _doc.Open();
        }

        public void StartWatchingImages()
        {
            _logger.Info("ImageService started watching images.");

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
                watcher.Created += new FileSystemEventHandler(OnImageCreatedHandler);

                // Begin watching.
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception exception)
            {
                _logger.Error($"Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        public void StopWatchingImages()
        {
            _logger.Info("ImageService is stopping watching for images.");

            try
            {
                _doc.Close();
            }
            catch (Exception exception)
            {
                _logger.Error($"Error has occured during closing PDF document. Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }

            _logger.Info("PDF document was closed.");
        }

        private void OnImageCreatedHandler(object source, FileSystemEventArgs e)
        {
            _logger.Info($"ImageService discovered a new file. File: {e.FullPath} {e.ChangeType}{Environment.NewLine}");

            try
            {
                // The problem is that the FileSystemWatcher tells you immediately when
                // the file was created. It doesn't wait for the file to be released.
                // The system is still creating the file when you already have the message,
                // which is why you are getting that error message. Basically, you need to
                // wait your turn. Take the event and then have a utility watch that file
                // until it is free for reading.
                _fileService.WaitUntilFileIsReleased(e.FullPath);

                int numberOfCurrentImage = _fileService.ExtractNumberFromFileName(e.Name);

                _logger.Info($"The number of the current file is {numberOfCurrentImage}.");

                if (_isFirstFile)
                {
                    _isFirstFile = false;
                }
                else if (_numberOfLastImage + 1 != numberOfCurrentImage)
                {
                    _doc.Close();

                    byte[] result = _memoryStream.ToArray();
                    _centerQueueClient.Send(result);

                    _doc = _pdfDocumentService.CreateNextPdfDocument(ref _memoryStream);
                    _doc.Open();
                }

                var image = Image.GetInstance(e.FullPath);
                _doc.AddImage(image);
                _numberOfLastImage = numberOfCurrentImage;

            }
            catch (IOException ioException)
            {
                _logger.Error($"Error message: {ioException.Message}{Environment.NewLine}StackTrace: {ioException.StackTrace}");
                _logger.Error($"The file {e.Name} will be moved to a separate folder.");

                _fileService.MoveFileToFolder(
                    e.Name,
                    e.FullPath,
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        ConfigurationManager.AppSettings["notLoadedImagesFolderName"]));
            }
            catch (Exception exception)
            {
                _logger.Error($"Error has occured during adding image to a PDF document. Error message: {exception.Message}{Environment.NewLine}StackTrace: {exception.StackTrace}");
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _memoryStream.Dispose();
            }
        }
    }
}
