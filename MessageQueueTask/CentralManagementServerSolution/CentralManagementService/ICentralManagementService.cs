using CentralManagementService.Messages;
namespace CentralManagementService
{
    public interface ICentralManagementService
    {
        void StartCentralQueueProcessing();
        void StopCentralQueueProcessing();
        void SendBroadcastMessage(BaseMessage message);
    }
}
