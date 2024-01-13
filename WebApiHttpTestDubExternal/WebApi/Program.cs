using WebApi.HttpClients;

namespace WebApi
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // adding appsettings.json configuration file
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);
            builder.Configuration.AddConfiguration(configuration.Build());

            // add http client support
            builder.Services.AddHttpClient(Clients.CrmApiClient, client =>
            {
                client.BaseAddress = new Uri("http://localhost:5052");
            });

            // add the http client(s)
            // NOTE that the client DI will be replaced in the integration test 
            builder.Services.AddSingleton<IAuthorClient, AuthorClient>();

            // add swagger support
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // add controller support
            builder.Services.AddControllers();

            var app = builder.Build();

            // use Swagger only in development
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // get a configuration value example
            var value = builder.Configuration.GetValue<string>("AppSettings:Key1");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}