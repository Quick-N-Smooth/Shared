## Mocking/stubbing/faking services for http tests

```
Scenario:
   Given that the user asks for the list of authors
   When the request arrives
   Then the application gets the authors list from the Authors Http Server
```

The following sample application gives a solution for using
mocks, fakes or stub in a web api http test for **dubbing the external services**.

*The system test differs from the integration test in such a way that it simulates the whole system
with dubbed read data from external (http or database) services.*

The problem with system testing such a solution is that in real life all
the external services must be instanciated before the test is called. When the solution ruin
through a postman of from browser, then all the external services instanciated and it runs on real data. 
However when the WebApi run by test a framework, ONLY the WebApi is started.

The solution is to dub the Client agent and making the Client to return a local testdata.

How it works:
The solution fakes the IAuthorClient external interface and for the integration
test it replaces the AuthorClient implementation into a fake.
The original ```AuthorClient``` in the WebApi project is replaced by the ```AuthorClient```
in the IntegrationTest project.
See how it is done in the ```AuthorsHttpTests``` testclass:

```
    var webAppFactory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
    {
        builder.ConfigureTestServices(services =>
        {
            services.Replace(new ServiceDescriptor(typeof(IAuthorClient), typeof(Dubbed.AuthorClient), ServiceLifetime.Singleton));
        });
    });
```


