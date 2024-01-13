using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using WebApi.Middlewares;

namespace WebApi.Tests;

/// <summary>
/// Test cases when 
/// Request header checking middleware is OFF
/// Exception handling middleware is ON
/// </summary>
[TestClass]
public class ExceptionThrownInControllerTests
{
    private readonly WebApplicationBuilder builder;
    private HttpClient _httpClient;

    public ExceptionThrownInControllerTests()
    {
        var webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.Replace(new ServiceDescriptor(typeof(ICheckRequestCultureService), typeof(CheckRequestCultureServiceDummy), ServiceLifetime.Singleton));
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
    /// Exception thrown in the controller
    /// The exception handling middleware handles the exception.
    /// </summary>
    [TestMethod]
    public async Task ExceptionThrownInController_HandledInMiddleware()
    {
        var response = await _httpClient.GetAsync("api/Author/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from Controller"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in middleware."));
    }

    /// <summary> 
    /// Exception filter is activated.
    /// Exception thrown in the controller
    /// The exception handling filter handles the exception.
    /// The exception handling middleware let the handled exception go!!!!.
    /// IT IS POSSIBLE TO USE BOTH THE FILTER ATTRIBUTE AND THE MIDDLEWARE TOGETHER
    /// </summary>
    [TestMethod]
    public async Task ExceptionThrownInController_HandledInFilter()
    {
        var response = await _httpClient.GetAsync("api/AuthorWithExceptionFilter/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from Controller"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in FilterAttribute"));
    }
}