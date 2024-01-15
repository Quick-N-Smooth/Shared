using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net;

namespace WebApi.Tests;

/// <summary>
/// Test cases when 
/// Request header checking middleware is ON
/// Exception handling middleware is ON
/// </summary>
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

    /// <summary>
    /// Etalon happy flow A1 showing the normal working flow
    /// </summary>
    [TestMethod]
    public async Task HappyFlow()
    {
        _httpClient.DefaultRequestHeaders.Add("culture", "sv-SE");

        var response = await _httpClient.GetAsync("api/Author/0");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Etalon happy flow A2 showing the normal working flow
    /// </summary>
    [TestMethod]
    public async Task HappyFlowWithExceptionFilter()
    {
        _httpClient.DefaultRequestHeaders.Add("culture", "sv-SE");

        var response = await _httpClient.GetAsync("api/AuthorWithExceptionFilter/0");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary> 
    /// No exception filter is activated.
    /// Exception thrown in the middleware
    /// The exception handling middleware handles the exception.
    /// </summary>
    [TestMethod]
    public async Task ExceptionThrownInMiddleware_HandledInMiddleware()
    {
        var response = await _httpClient.GetAsync("api/Author/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from Middleware"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in Middleware"));
    }

    /// <summary> 
    /// Exception filter is activated.
    /// Exception thrown in the middleware
    /// ONLY THE EXCEPTION HANDLER MIDDLEWARE CATCHES THE EXCEPTION!!!
    /// </summary>
    [TestMethod]
    public async Task ExceptionThrownInMiddleware_ExceptionFilterIsActive()
    {
        var response = await _httpClient.GetAsync("api/AuthorWithExceptionFilter/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from Middleware"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in Middleware"));
    }
}