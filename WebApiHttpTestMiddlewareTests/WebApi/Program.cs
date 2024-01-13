using Omocom.BackOffice.Api.Filters;
using WebApi.Middlewares;

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

            // add first the middlewares as services
            builder.Services.AddExceptionHandlingService();
            builder.Services.AddCheckRequestCultureService();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // add controller support
            builder.Services.AddControllers();

            var app = builder.Build();

            // add middlewares support
            // NOTE that we instanciated a service as a first step
            // DI will then insert the service into the middleware and 
            // also the middleware gets testable by replacing the service in the test class
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<CheckRequestCultureMiddleware>();

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