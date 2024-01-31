namespace DocumentStorageEasyAppDemo.DocumentStorageModule
{
    public interface IDocumentStorageService
    {
        Task DocumentAdd(Document document);

        Task DocumentUpdate(string id, string[] tags, dynamic data);

        Task<byte[]> Document(string id, string requestContentType);
    }
}
