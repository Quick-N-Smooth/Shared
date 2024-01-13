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
    public class AuthorsSuccessfulHttpTests
    {
        private readonly WebApplicationBuilder builder;
        private HttpClient _httpClient;

        public AuthorsSuccessfulHttpTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(new ServiceDescriptor(typeof(IAuthorClient), typeof(Dubbed.DubbedAuthorClient), ServiceLifetime.Singleton));
                });
            });
            _httpClient = webAppFactory.CreateDefaultClient();
            this.builder = builder;
        }

        [TestMethod]
        public async Task Authors_ReturnsValidResponse()
        {
            var response = await _httpClient.GetAsync("api/Authors");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResult = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonSerializer.Deserialize<List<AuthorViewModel>>(stringResult);
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual(2, jsonResult.Count);
        }

        [TestMethod]
        public async Task Author_Found_ReturnsValidResponse()
        {
            var response = await _httpClient.GetAsync("api/Authors/Aut03");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResult = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonSerializer.Deserialize<AuthorViewModel>(stringResult);
            Assert.IsNotNull(jsonResult);
            Assert.AreEqual("Benjamin Broom", jsonResult.FullName);
        }


        [TestMethod]
        public async Task Author_NotFound_ReturnsValidHttpCode()
        {
            var response = await _httpClient.GetAsync("api/Authors/Aut05");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}