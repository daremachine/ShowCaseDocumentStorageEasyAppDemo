using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace DocumentStorageEasyAppDemo.Core.DocumentTypes.Types
{
    public class DocumentTypeXML : IDocumentType
    {
        public string ContentType => "application/xml";

        public byte[] ConvertDataType(string data, string contentType)
        {
            if (contentType != ContentType) return Array.Empty<byte>();

            try
            {
                XNode result = JsonConvert.DeserializeXNode(data, "Root");
                return Encoding.UTF8.GetBytes(result?.ToString());
            }
            catch (JsonReaderException)
            {
                return Encoding.UTF8.GetBytes(data);
            }
        }
    }
}
