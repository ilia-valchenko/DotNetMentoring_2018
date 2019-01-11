using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace MessageQueueTask.PdfDocumentService
{
    public class PdfDocumentService : IPdfDocumentService
    {
        private int _numberOfPdfDocument = 0;
        private readonly Logger.ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfDocumentService"/> class.
        /// </summary>
        public PdfDocumentService(Logger.ILogger logger)
        {
            _logger = logger;
        }

        public Document CreateNextPdfDocument(ref MemoryStream memoryStream)
        {
            _logger.Info($"Create a new PDF document at number {++_numberOfPdfDocument}.");

            int nextDocumentNumber = ++_numberOfPdfDocument;
            var doc = new Document();

            if(memoryStream != null)
            {
                memoryStream.Flush();
                memoryStream.Dispose();
            }

            memoryStream = new MemoryStream();
            PdfWriter.GetInstance(doc, memoryStream);

            _logger.Info($"The new PDF document at number {nextDocumentNumber} was created.");

            return doc;
        }
    }
}
