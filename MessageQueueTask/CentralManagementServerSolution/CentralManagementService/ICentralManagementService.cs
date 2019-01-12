using MessageQueueTask.MessagesLibrary.CentralServiceMessages;

namespace CentralManagementService
{
    public interface ICentralManagementService
    {
        void StartCentralQueueProcessing();
        void StopCentralQueueProcessing();
        void SendBroadcastMessage(BaseMessage message);
    }
}
