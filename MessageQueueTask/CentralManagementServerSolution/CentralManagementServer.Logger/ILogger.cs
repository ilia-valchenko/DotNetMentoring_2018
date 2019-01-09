using System;

namespace CentralManagementServer.Logger
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
        void Error(Exception exception);
        void Error(string message, Exception exception);
    }
}
