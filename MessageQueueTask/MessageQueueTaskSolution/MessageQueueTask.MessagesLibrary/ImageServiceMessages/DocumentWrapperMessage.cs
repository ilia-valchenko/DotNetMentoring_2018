using System;

namespace MessageQueueTask.MessagesLibrary.ImageServiceMessages
{
    /// <summary>
    /// This class is wrapper for the iTextSharp document as its document
    /// does not have empty constructor and cannot be serialized.
    /// </summary>
    [Serializable]
    public class DocumentWrapperMessage
    {
        public byte[] ITextSharpDocumentBytes { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="DocumentWrapperMessage"/>
        /// class.
        /// </summary>
        public DocumentWrapperMessage() { }
    }
}
