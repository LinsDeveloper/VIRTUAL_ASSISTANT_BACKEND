using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Data;
using VIRTUAL_ASSISTANT.Application.Interfaces.Providers;
using VIRTUAL_ASSISTANT.Application.Services.Providers;
using VIRTUAL_ASSISTANT.Domain.Arguments.Configurations;
using VIRTUAL_ASSISTANT.Domain.DTO;
using VIRTUAL_ASSISTANT.Domain.Interfaces;
using VIRTUAL_ASSISTANT.Infra;
using VIRTUAL_ASSISTANT.Infra.Persistence.UnitOfWork;

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

                    services.AddScoped<IUserContextProvider, HttpUserContextProvider>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    ;
                    services.AddScoped((provider) =>
                    {
                        var connection = provider.GetRequiredService<IDbConnection>();
                        var optionsBuilder = new DbContextOptionsBuilder<ContextManager>();
                        optionsBuilder.UseMySQL(connection.ConnectionString);

                        return new ContextManager(optionsBuilder.Options);
                    });

                        var httpClientSettings = context.Configuration.GetSection("HttpClientServices").Get<Dictionary<string, HttpClientConfig>>()
                             ?? throw new InvalidOperationException("HttpClientservices connot be null");

                        foreach (var client in httpClientSettings)
                        {
                            var clientName = client.Key;
                            var clientConfig = client.Value;

                            if (string.IsNullOrWhiteSpace(clientConfig.BaseAddress) ||
                                !Uri.TryCreate(clientConfig.BaseAddress, UriKind.Absolute, out var uriResult) ||
                                (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
                            {
                                throw new InvalidOperationException($"Invalid base address for {clientName}");
                            }

                            services.AddHttpClient(clientName, client =>
                            {
                                client.BaseAddress = new Uri(clientConfig.BaseAddress);
                                client.DefaultRequestHeaders.Add("Accept", "application/json");
                                client.Timeout = TimeSpan.FromSeconds(clientConfig.TimeoutSeconds);
                            });
                        }
                })
                .Build();

            await host.RunAsync();
        }
    }
}