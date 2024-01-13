using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using WebApi.Middlewares;

namespace WebApi.Tests;

[TestClass]
public class ExceptionThrownInMiddlewareTests
{
    private readonly WebApplicationBuilder builder;
    private HttpClient _httpClient;

    public ExceptionThrownInMiddlewareTests()
    {
        var webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // no replace
            });
        });
        _httpClient = webAppFactory.CreateDefaultClient();
        this.builder = builder;
    }

    [TestMethod]
    public async Task HappyFlow()
    {
        _httpClient.DefaultRequestHeaders.Add("culture", "sv-SE");

        var response = await _httpClient.GetAsync("api/Author/0");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task HappyFlowWithExceptionFilter()
    {
        _httpClient.DefaultRequestHeaders.Add("culture", "sv-SE");

        var response = await _httpClient.GetAsync("api/AuthorWithExceptionFilter/0");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task ExceptionThrownInMiddleware_HandledInMiddleware()
    {
        var response = await _httpClient.GetAsync("api/Author/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from middleware"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in middleware"));
    }

    [TestMethod]
    public async Task ExceptionThrownInMiddleware_ExceptionFilterIsActive()
    {
        var response = await _httpClient.GetAsync("api/AuthorWithExceptionFilter/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from middleware"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in middleware"));
    }
}