using DocumentStorageEasyAppDemo.Core.DocumentTypes.Types;

namespace DocumentStorageEasyAppDemo.Core.DocumentTypes
{
    public sealed class DocumentTypeConverter : IDocumentTypeConverter
    {
        private readonly Dictionary<string, IDocumentType> _converters = new();

        public void RegisterDocumentType(Type type)
        {
            if (typeof(IDocumentType).IsInstanceOfType(type))
                throw new ArgumentNullException($"{type.FullName} must implement IDocumentType");

            IDocumentType? instance = Activator.CreateInstance(type) as IDocumentType;
            if (instance == null) throw new InvalidOperationException();

            if (instance != null && _converters.ContainsKey(instance.ContentType))
                throw new ContentTypeAlreadyRegisteredException(instance.ContentType);

            _converters.Add(instance.ContentType, instance);
        }

        public IDocumentType GetDocumentType(string contentType)
        {
            var result = _converters.FirstOrDefault(_ => _.Key == contentType).Value;
            if (result == null) throw new TypeConverterNotRegisteredException(contentType);

            return result;
        }
    }
}
