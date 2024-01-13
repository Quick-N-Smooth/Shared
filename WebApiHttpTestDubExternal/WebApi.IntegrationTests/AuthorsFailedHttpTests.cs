using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Text.Json;
using WebApi.HttpClients;
using WebApi.Tests.Dubbed;
using WebApi.ViewModels;

namespace WebApi.Tests
{
    [TestClass]
    public class AuthorsFailedHttpTests
    {
        private readonly WebApplicationBuilder builder;
        private HttpClient _httpClient;

        public AuthorsFailedHttpTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(new ServiceDescriptor(typeof(IAuthorClient), typeof(Dubbed.DubbedAuthorClientWithException), ServiceLifetime.Singleton));
                });
            });
            _httpClient = webAppFactory.CreateDefaultClient();
            this.builder = builder;
        }

        [TestMethod]
        public async Task Authors_TechnicalError_ReturnsInternalServerErrorWithErrorText()
        {
            var response = await _httpClient.GetAsync("api/Authors");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(stringResult.Contains("Test exception"));
        }

        [TestMethod]
        public async Task Author_TechnicalError_ReturnsInternalServerErrorWithErrorText()
        {
            var response = await _httpClient.GetAsync("api/Authors/Aut03");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(stringResult.Contains("Test exception"));
        }
    }
}