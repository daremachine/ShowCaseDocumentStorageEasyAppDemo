using DocumentStorageEasyAppDemo.Core;
using DocumentStorageEasyAppDemo.Core.DocumentTypes;
using DocumentStorageEasyAppDemo.Tests.Fixtures;

namespace DocumentStorageEasyAppDemo.Tests
{
    public class DocumentTypeConverterTest
    {
        public const string ContentType = "plain/text";
        public const string TestText = "Test";

        [Fact]
        public void GetRightConverterType()
        {
            var converter = new DocumentTypeConverter();
            converter.RegisterDocumentType(typeof(DocumentTypeTest));


            var type = converter.GetDocumentType(ContentType);
            var result = type.ConvertDataType(TestText, ContentType);

            Assert.Equal(typeof(byte[]), result.GetType());
        }

        [Fact]
        public void ConverterTypeNotRegistered()
        {
            var contentType = "application/json";
            var converter = new DocumentTypeConverter();
            converter.RegisterDocumentType(typeof(DocumentTypeTest));

            Assert.Throws<TypeConverterNotRegisteredException>(() =>
            {
                var type = converter.GetDocumentType(contentType);
            });
        }

        [Fact]
        public void ContentTypeAlreadyRegistered()
        {
            var converter = new DocumentTypeConverter();

            Assert.Throws<ContentTypeAlreadyRegisteredException>(() =>
            {
                converter.RegisterDocumentType(typeof(DocumentTypeTest));
                converter.RegisterDocumentType(typeof(DocumentTypeTest));
            });
        }
    }
}