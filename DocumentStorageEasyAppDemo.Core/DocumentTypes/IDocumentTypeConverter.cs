using DocumentStorageEasyAppDemo.Core.DocumentTypes.Types;

namespace DocumentStorageEasyAppDemo.Core.DocumentTypes
{
    public interface IDocumentTypeConverter
    {
        public void RegisterDocumentType(Type type);

        public IDocumentType GetDocumentType(string contentType);
    }
}
