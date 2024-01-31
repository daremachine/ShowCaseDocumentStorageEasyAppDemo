using DocumentStorageEasyAppDemo.Core.StorageProviders;
using DocumentStorageEasyAppDemo.DocumentStorageModule;
using DocumentStorageEasyAppDemo.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace DocumentStorageEasyAppDemo.Tests
{
    public class DocumentEndpointTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private const string UrlPrefix = "/documents";

        public DocumentEndpointTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task DocumentAddTest()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.ContentType), "application/json");
            var id = "test";

            var result = await client.PostAsJsonAsync(UrlPrefix, new DocumentModel
            {
                Id = id,
                Tags = new string[] { "test", "bbb" },
                Data = new
                {
                    a = "b",
                    c = "d"
                }
            });

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            var storageProvider = _factory.Services.GetRequiredService<IStorageProvider>();

            Assert.NotNull(storageProvider);

            var document = await storageProvider.Get(id);
            Assert.NotNull(document);
            Assert.Equal(id, document.Id);
        }

        [Fact]
        public async Task DocumentGetTest()
        {
            var client = _factory.CreateClient();

            // add test data
            var storageProvider = _factory.Services.GetRequiredService<IStorageProvider>();
            var document = await storageProvider.Add(new Document("test", new string[] { "test", "bbb" }, "testdata"));

            var request = new HttpRequestMessage(HttpMethod.Get, $"{UrlPrefix}/{document.Id}");
            request.Headers.Add(nameof(HttpRequestHeader.ContentType), "plain/text");

            var result = await client.SendAsync(request);

            Assert.NotNull(result);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            Assert.Equal("plain/text", result.Content.Headers.ContentType?.ToString());

            Assert.Equal("testdata", await result.Content.ReadAsStringAsync());
        }
    }
}
