using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using WebApi.ViewModels;

namespace WebApi.Tests
{
    [TestClass]
    public class IsAliveHttpTests
    { 
        private HttpClient _httpClient;
        public IsAliveHttpTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task IsAlive_ReturnsValidResponse()
        {
            var response = await _httpClient.GetAsync("api/IsAlive");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var stringResult = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonSerializer.Deserialize<IsAliveViewModel>(stringResult);
            Assert.IsNotNull(jsonResult);
            Assert.AreNotEqual(DateTime.MinValue, jsonResult.DateTime);
        }
    }
}