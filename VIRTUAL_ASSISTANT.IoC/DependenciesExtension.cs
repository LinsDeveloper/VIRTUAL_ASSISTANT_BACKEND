using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using VIRTUAL_ASSISTANT.Domain.Arguments.Configurations;
using VIRTUAL_ASSISTANT.Infra;

namespace VIRTUAL_ASSISTANT.IoC
{
    public static class DependenciesExtension
    {

        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped((provider) =>
            {
                var connection = provider.GetRequiredService<IDbConnection>();
                var optionsBuilder = new DbContextOptionsBuilder<ContextManager>();
                optionsBuilder.UseMySQL(connection.ConnectionString);

                return new ContextManager(optionsBuilder.Options);
            });
        }


        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var httpClientSettings = configuration.GetSection("HttpClientServices").Get<Dictionary<string, HttpClientConfig>>()
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
        }
    }
}
