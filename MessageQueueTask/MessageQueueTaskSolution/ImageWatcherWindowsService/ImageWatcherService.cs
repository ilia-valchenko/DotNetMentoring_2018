using MessageQueueTask.ImageWatcherService;
using MessageQueueTask.Logger;
using System.ServiceProcess;

namespace ImageWatcherWindowsService
{
    public partial class ImageWatcherService : ServiceBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger _logger;

        public ImageWatcherService()
        {
            InitializeComponent();
            _logger = new Logger();
            _imageService = new ImageService(_logger);
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("ImageWatcherWindowsService started.");
            _imageService.StartWatchingImages();

            // 2. Create connect to message queue
        }

        protected override void OnStop()
        {
            _logger.Info("ImageWatcherWindowsService is stopping.");
            _imageService.StopWatchingImages();
            _logger.Info("ImageWatcherWindowsService stopped.");
        }
    }
}
