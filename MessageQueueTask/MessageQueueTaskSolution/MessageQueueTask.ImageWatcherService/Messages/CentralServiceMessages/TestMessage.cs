namespace MessageQueueTask.ImageWatcherService.Messages
{
    public class TestMessage : BaseMessage
    {
        public string Text { get; set; }

        public TestMessage() { }
    }
}
