using iTextSharp.text;

namespace MessageQueueTask.PdfDocumentService.Extensions
{
    public static class PdfDocumentExtension
    {
        public static void AddImage(
            this Document document,
            Image img,
            int alignment = Element.ALIGN_CENTER)
        {
            img.Alignment = alignment;
            document.Add(img);
        }
    }
}
