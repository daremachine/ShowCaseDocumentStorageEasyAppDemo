using DocumentStorageEasyAppDemo.DocumentStorageModule;
using Microsoft.EntityFrameworkCore;

namespace DocumentStorageEasyAppDemo.Core.StorageProviders
{
    public class StorageProviderInMemory : IStorageProvider
    {
        public StorageProviderInMemory()
        {
        }

        public async Task<Document> Add(Document document)
        {
            using var context = new InMemoryDb();

            context.Documents.Add(document);
            await context.SaveChangesAsync();

            return document;
        }

        public async Task<Document?> Get(string id)
        {
            using var context = new InMemoryDb();

            var document = await context.Documents.SingleOrDefaultAsync(_ => _.Id == id);

            return document;
        }

        public async Task<Document> Update(string id, string[] tags, string data)
        {
            using var context = new InMemoryDb();

            var document = await context.Documents.SingleOrDefaultAsync(_ => _.Id == id);
            if (document == null) throw new DocumentNotFoundException(id);

            document.UpdateData(tags, data);

            context.Update(document);

            await context.SaveChangesAsync();

            return document;
        }
    }
}
