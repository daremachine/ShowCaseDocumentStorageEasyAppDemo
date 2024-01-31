using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DocumentStorageEasyAppDemo.Tests.Fixtures
{
    internal class WebClientFactory
    {
        public static async Task<HttpClient> Create()
        {
            await using var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();

            return client;
        }
    }
}
