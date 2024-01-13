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
/// NO Exception handling middleware AND
/// NO Request header checking middleware activated
/// </summary>
[TestClass]
public class NoMiddlewareExceptionHandling
{
    private readonly WebApplicationBuilder builder;
    private HttpClient _httpClient;

    public NoMiddlewareExceptionHandling()
    {
        var webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                // replace the middleware logic into a dummy funtion
                services.Replace(new ServiceDescriptor(typeof(IExceptionHandlingService), typeof(ExceptionHandlingServiceDummy), ServiceLifetime.Singleton));
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
    /// No middlewares, 
    /// no exception filter is activated.
    /// Exception thrown in the controller
    /// Only the default API core global handler catches it.
    /// </summary>
    [TestMethod]
    public async Task ExceptionThrownInController_NoExceptionFilter_GlobalExceptionHandler()
    {
        // call to the controller with no exception filter
        var response = await _httpClient.GetAsync("api/Author/0");
        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

        // we get a normal system message
        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("System.ArgumentNullException: Value cannot be null"));
    }

    /// <summary>
    /// No middlewares, 
    /// Exception filter attribute is activated.
    /// Exception thrown in the controller
    /// Exception filter attribute catches it.
    /// </summary>
    [TestMethod]
    public async Task ExceptionThrownInController_ExceptionFilterIsActive()
    {
        // call to the controller with exception filter
        var response = await _httpClient.GetAsync("api/AuthorWithExceptionFilter/0");
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

        var stringResult = await response.Content.ReadAsStringAsync();
        Assert.IsTrue(stringResult.Contains("Argument null exception from Controller"));
        Assert.IsTrue(stringResult.Contains("ArgumentNullException handled in FilterAttribute"));
    }
}