using iTextSharp.text;
using System.IO;

namespace MessageQueueTask.PdfDocumentService
{
    public interface IPdfDocumentService
    {
        Document CreateNextPdfDocument(ref MemoryStream memoryStream);
    }
}
