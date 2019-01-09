namespace CentralManagementService
{
    public interface ICentralManagementService
    {
        void StartCentralQueueProcessing();
        void StopCentralQueueProcessing();
    }
}
