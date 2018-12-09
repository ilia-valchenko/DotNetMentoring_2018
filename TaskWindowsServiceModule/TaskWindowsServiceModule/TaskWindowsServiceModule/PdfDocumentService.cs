using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.IO;

namespace TaskWindowsServiceModule
{
    /// <summary>
    /// The PDF document service.
    /// </summary>
    public class PdfDocumentService
    {
        private int _numberOfPdfDocument = 0;
        private readonly SimpleLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfDocumentService"/> class.
        /// </summary>
        public PdfDocumentService()
        {
            _logger = new SimpleLogger();
        }

        public Document CreateNextPdfDocument()
        {
            string documentName = ConfigurationManager.AppSettings["pdfFileName"];
            int nextDocumentNumber = ++_numberOfPdfDocument;
            var doc = new Document();

            _logger.Log($"Create a new PDF document {documentName + nextDocumentNumber}");

            PdfWriter.GetInstance(
                doc,
                new FileStream(
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        documentName + nextDocumentNumber.ToString() + ".pdf"),
                    FileMode.Create));

            return doc;
        }
    }
}
