namespace MessageQueueTask.ImageWatcherService
{
    public interface IImageService
    {
        void StartWatchingImages();
        void StopWatchingImages();
        void StartListenBroadcastMessagesAsync();
    }
}
