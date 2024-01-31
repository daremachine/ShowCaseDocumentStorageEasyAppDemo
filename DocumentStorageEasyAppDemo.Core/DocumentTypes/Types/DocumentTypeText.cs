using System.Text;

namespace DocumentStorageEasyAppDemo.Core.DocumentTypes.Types
{
    public class DocumentTypeText : IDocumentType
    {
        public string ContentType => "plain/text";

        public byte[] ConvertDataType(string data, string contentType)
        {
            if (contentType != ContentType) return Array.Empty<byte>();

            return Encoding.UTF8.GetBytes(data);
        }
    }
}
