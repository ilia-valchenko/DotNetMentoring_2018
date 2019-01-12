namespace MessageQueueTask.ImageWatcherService.Messages
{
    public class NewSettingsMessage : BaseMessage
    {
        public string TestValue { get; set; }
    }
}
