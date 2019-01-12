using MessageQueueTask.Core;

namespace MessageQueueTask.ImageWatcherService
{
    public enum ImageServiceActions
    {
        [Text("WatchingForImages")]
        WatchingForImages,

        [Text("AddImageToDocument")]
        AddImageToDocument,

        [Text("CreateNewDocument")]
        CreateNewDocument,

        [Text("CloseDocument")]
        CloseDocument,

        [Text("MoveBadImageToFolder")]
        MoveBadImageToFolder,

        [Text("WaitUntilFileIsReleased")]
        WaitUntilFileIsReleased,

        [Text("ReadBroadcastMessage")]
        ReadBroadcastMessage
    }
}
