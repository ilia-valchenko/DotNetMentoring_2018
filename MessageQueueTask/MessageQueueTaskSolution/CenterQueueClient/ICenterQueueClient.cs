namespace CenterQueueClient
{
    public interface ICenterQueueClient
    {
        void Send(object message);
    }
}
