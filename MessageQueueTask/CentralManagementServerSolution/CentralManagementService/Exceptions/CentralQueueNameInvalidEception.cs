using System;

namespace CentralManagementService.Exceptions
{
    public class CentralQueueNameInvalidEception : Exception
    {
        public CentralQueueNameInvalidEception(): base() {}

        public CentralQueueNameInvalidEception(string message): base(message) {}
    }
}
