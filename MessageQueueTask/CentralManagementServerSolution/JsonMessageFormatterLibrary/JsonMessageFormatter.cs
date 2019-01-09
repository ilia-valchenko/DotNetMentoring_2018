using System;
using System.IO;
using System.Messaging;
using System.Text;

namespace JsonMessageFormatterLibrary
{
    /// <summary>
    /// The JSON message formatter.
    /// </summary>
    public class JsonMessageFormatter<T> : IMessageFormatter
    {
        private readonly Encoding _encoding;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonMessageFormatter{T}"/> class.
        /// </summary>
        public JsonMessageFormatter() : this(Encoding.UTF8) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonMessageFormatter{T}"/> class.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        public JsonMessageFormatter(Encoding encoding)
        {
            _encoding = encoding;
        }

        public bool CanRead(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var stream = message.BodyStream;

            return stream != null
                && stream.CanRead
                && stream.Length > 0;
        }

        public object Clone()
        {
            return new JsonMessageFormatter<T>(_encoding);
        }

        public object Read(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            using (var reader = new StreamReader(message.BodyStream, _encoding))
            {
                var json = reader.ReadToEnd();
                return NetJSON.NetJSON.Deserialize<T>(json);
            }
        }

        public void Write(Message message, object obj)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            string json = NetJSON.NetJSON.Serialize(obj);
            message.BodyStream = new MemoryStream(_encoding.GetBytes(json));
        }
    }
}
