using DocumentStorageEasyAppDemo.Core.StorageProviders;
using DocumentStorageEasyAppDemo.DocumentStorageModule;
using DocumentStorageEasyAppDemo.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
            var id = "DocumentAddTest";

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

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            var storageProvider = _factory.Services.GetRequiredService<IStorageProvider>();

            Assert.NotNull(storageProvider);

            var document = await storageProvider.Get(id);
            Assert.NotNull(document);
            Assert.Equal(id, document.Id);
        }

        [Fact]
        public async Task DocumentAddDuplicateTest()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.ContentType), "application/json");
            var document = new Document("DocumentAddDuplicateTest", Array.Empty<string>(), string.Empty);

            var storageProvider = _factory.Services.GetRequiredService<IStorageProvider>();
            await storageProvider.Add(document);

            var result = await client.PostAsJsonAsync(UrlPrefix, new DocumentModel
            {
                Id = document.Id
            });

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        }

        private (HttpClient client, IServiceProvider services) _DocumentAddDuplicateTest2_Fixture()
        {
            var factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var mockService = new Mock<IStorageProvider>();
                    mockService.Setup(s => s.Get(It.IsAny<string>())).ReturnsAsync(new Document(
                        "DocumentAddDuplicateTest2",
                        Array.Empty<string>(),
                        "data"));

                    services.AddScoped<IStorageProvider>(_ => mockService.Object);
                });
            });

            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.ContentType), "application/json");

            return (client, factory.Services);
        }

        [Fact]
        public async Task DocumentAddDuplicateTest2()
        {
            (HttpClient client, IServiceProvider services) = _DocumentAddDuplicateTest2_Fixture();
            var id = "DocumentAddDuplicateTest2";

            var result = await client.PostAsJsonAsync(UrlPrefix, new DocumentModel
            {
                Id = id
            });

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);

            using var scope = services.CreateScope();
            var storageProvider = scope.ServiceProvider.GetRequiredService<IStorageProvider>();
            var mockStorageProvider = Mock.Get(storageProvider);
            mockStorageProvider.Verify(m => m.Get(id), Times.Once);
        }


        [Fact]
        public async Task DocumentGetTest()
        {
            var client = _factory.CreateClient();

            // add test data
            var storageProvider = _factory.Services.GetRequiredService<IStorageProvider>();
            var document = await storageProvider.Add(new Document("DocumentGetTest", new string[] { "test", "bbb" }, "testdata"));

            var request = new HttpRequestMessage(HttpMethod.Get, $"{UrlPrefix}/{document.Id}");
            request.Headers.Add(nameof(HttpRequestHeader.ContentType), "plain/text");

            var result = await client.SendAsync(request);

            Assert.NotNull(result);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            Assert.Equal("plain/text", result.Content.Headers.ContentType?.ToString());

            Assert.Equal("testdata", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task DocumentGetNotFoundTest()
        {
            var client = _factory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{UrlPrefix}/not_found_id");
            request.Headers.Add(nameof(HttpRequestHeader.ContentType), "plain/text");

            var result = await client.SendAsync(request);

            Assert.NotNull(result);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
