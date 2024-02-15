using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentStorageEasyAppDemo.Core
{
    public class DocumentNotFoundException : Exception
    {
        public DocumentNotFoundException(string id) : base($"Document {id} not found") { }
    }
    public class DocumentAlreadyExistWithIdException : Exception
    {
        public DocumentAlreadyExistWithIdException(string id) : base($"Document already exist with {id}") { }
    }

    public class TypeConverterNotRegisteredException : Exception
    {
        public TypeConverterNotRegisteredException(string requestContentType) : base($"TypeConverter for type {requestContentType} not found") { }
    }

    public class ContentTypeAlreadyRegisteredException : Exception
    {
        public ContentTypeAlreadyRegisteredException(string requestContentType) : base($"TypeConverter with content typ {requestContentType} is already registered") { }
    }
}
