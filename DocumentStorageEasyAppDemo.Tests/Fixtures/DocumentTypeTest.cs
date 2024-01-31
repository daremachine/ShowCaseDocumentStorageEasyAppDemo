using DocumentStorageEasyAppDemo.Core.DocumentTypes.Types;
using System.Text;

namespace DocumentStorageEasyAppDemo.Tests.Fixtures
{
    internal class DocumentTypeTest : IDocumentType
    {
        public string ContentType => "plain/text";

        public byte[] ConvertDataType(string data, string contentType)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }
}
