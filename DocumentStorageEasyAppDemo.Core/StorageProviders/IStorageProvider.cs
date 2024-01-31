using DocumentStorageEasyAppDemo.DocumentStorageModule;

namespace DocumentStorageEasyAppDemo.Core.StorageProviders
{
    public interface IStorageProvider
    {
        Task<Document> Add(Document document);

        Task<Document> Update(string id, string[] tags, string data);

        Task<Document?> Get(string id);
    }
}
