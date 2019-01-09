namespace MessageQueueTask.FileService
{
    public interface IFileService
    {
        void WaitUntilFileIsReleased(string fullPath);
        void MoveFileToFolder(string fileName, string filePath, string folderPath);
        int ExtractNumberFromFileName(string filename);
    }
}
