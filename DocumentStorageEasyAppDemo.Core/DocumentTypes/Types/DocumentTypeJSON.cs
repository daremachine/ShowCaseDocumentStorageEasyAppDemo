using Newtonsoft.Json;
using System.Text;

namespace DocumentStorageEasyAppDemo.Core.DocumentTypes.Types
{
    public class DocumentTypeJson : IDocumentType
    {
        public string ContentType => "application/json";

        public byte[] ConvertDataType(string data, string contentType)
        {
            if (contentType != ContentType) return Array.Empty<byte>();

            try
            {
                var obj = JsonConvert.DeserializeObject<object>(data);
                return Encoding.UTF8.GetBytes(obj.ToString());
            }
            catch (JsonReaderException)
            {
                return Encoding.UTF8.GetBytes(data);
            }
        }
    }
}
