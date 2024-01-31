namespace DocumentStorageEasyAppDemo.Core.DocumentTypes.Types
{
    public interface IDocumentType
    {
        string ContentType { get; }

        byte[] ConvertDataType(string data, string contentType);
    }
}
