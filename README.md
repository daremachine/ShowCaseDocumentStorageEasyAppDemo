# Assignment

>Create production-ready ASP.NET Core service app that provides API for storage and
retrive documents in different formats

1. The documents are send as a payload of POST request in JSON format and could be modified via PUT verb.
2. The service is able to return the stored documents in different format, such as XML,MessagePack, etc.
3. It must be easy to add support for new formats.
4. It must be easy to add different underlying storage, like cloud, HDD, InMemory, etc.
5. Assume that the load of this service will be very high (mostly reading).
6. Demonstrate ability to write unit tests.
7. The document has mandatory field id, tags and data as shown bellow. The schema of the data field can be arbitrary.

```
POST http://localhost:5000/documents
Content-Type: application/json; charset=UTF-8
{
  "id": "some-unique-identifier1",
  "tags": ["important", ".net"]
  "data": {
  "some": "data",
  "optional": "fields"
}
```

### get document
```
GET http://localhost:5000/documents/some-unique-identifier1
Accept: application/xml
```
