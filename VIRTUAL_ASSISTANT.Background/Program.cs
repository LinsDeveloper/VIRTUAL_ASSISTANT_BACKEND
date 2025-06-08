using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VIRTUAL_ASSISTANT.Background
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("integration.appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                          .AddEnvironmentVariables();

                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    var rabbitConfigSection = context.Configuration.GetSection("RabbitMQ");
                    services.Configure<ConfigRabbitMQ>(rabbitConfigSection);
;
                    services.AddTransient<HandlerIntegrationMessageArguments>();

                    services.AddScoped((provider) =>
                    {
                        var connection = provider.GetRequiredService<IDbConnection>();
                        var optionsBuilder = new DbContextOptionsBuilder<ContextManager>();
                        optionsBuilder.UseMySQL(connection.ConnectionString);

                        return new ContextManager(optionsBuilder.Options);
                    });
                })
                .Build();

            await host.RunAsync();
        }
    }
}