using DocumentStorageEasyAppDemo.Core;
using DocumentStorageEasyAppDemo.Core.DocumentTypes;
using DocumentStorageEasyAppDemo.Core.StorageProviders;

namespace DocumentStorageEasyAppDemo.DocumentStorageModule
{
    public class DocumentStorageService : IDocumentStorageService
    {
        private readonly IDocumentTypeConverter _documentTypeConverter;
        private readonly IStorageProvider _storageProvider;

        public DocumentStorageService(IDocumentTypeConverter providerStorageConverter,
            IStorageProvider storageProvider)
        {
            _documentTypeConverter = providerStorageConverter;
            _storageProvider = storageProvider;
        }

        public async Task<byte[]> Document(string id, string requestContentType)
        {
            var document = await _storageProvider.Get(id);

            if (document == null) throw new DocumentNotFoundException(id);

            var documentType = _documentTypeConverter.GetDocumentType(requestContentType);

            return documentType.ConvertDataType(document.Data, requestContentType);
        }

        public async Task DocumentAdd(Document document)
        {
            var documentExist = await _storageProvider.Get(document.Id);
            if (documentExist != null) throw new DocumentAlreadyExistWithIdException(document.Id);

            await _storageProvider.Add(document);
        }

        public async Task DocumentUpdate(string id, string[] tags, dynamic data)
        {
            await _storageProvider.Update(id, tags, data);
        }
    }
}
