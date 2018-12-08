using iTextSharp.text;

namespace TaskWindowsServiceModule.Extensions
{
    /// <summary>
    /// The class which contains extensions for the <see cref="iTextSharp.text.Document"/> class.
    /// </summary>
    public static class PdfDocumentExtension
    {
        /// <summary>
        /// Adds image with alignment to a PDF document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="img">The image.</param>
        /// <param name="alignment">The alignment value.</param>
        public static void AddImage(this Document document, Image img, int alignment = Element.ALIGN_CENTER)
        {
            img.Alignment = alignment;
            document.Add(img);
        }
    }
}
