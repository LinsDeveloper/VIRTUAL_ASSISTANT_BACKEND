using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using VIRTUAL_ASSISTANT.Application.Interfaces.Auth;
using VIRTUAL_ASSISTANT.Application.Interfaces.Providers;
using VIRTUAL_ASSISTANT.Application.Interfaces.UseCases;
using VIRTUAL_ASSISTANT.Application.Services.Auth;
using VIRTUAL_ASSISTANT.Application.Services.Providers;
using VIRTUAL_ASSISTANT.Application.UseCases.Users;
using VIRTUAL_ASSISTANT.Domain.Arguments.Configurations;
using VIRTUAL_ASSISTANT.Domain.Interfaces;
using VIRTUAL_ASSISTANT.Domain.Interfaces.Cache;
using VIRTUAL_ASSISTANT.Domain.Interfaces.Repository;
using VIRTUAL_ASSISTANT.Infra;
using VIRTUAL_ASSISTANT.Infra.Cache;
using VIRTUAL_ASSISTANT.Infra.Persistence.Repository.Base;
using VIRTUAL_ASSISTANT.Infra.Persistence.UnitOfWork;

namespace VIRTUAL_ASSISTANT.IoC
{
    public static class DependenciesExtension
    {

        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserContextProvider, HttpUserContextProvider>();
        }

        public static void AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUseCases, UserUseCase>();
            services.AddScoped<IReminderUseCase, ReminderUseCase>();
        }

        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped((provider) =>
            {
                var connection = provider.GetRequiredService<IDbConnection>();
                var optionsBuilder = new DbContextOptionsBuilder<ContextManager>();
                optionsBuilder.UseMySQL(connection.ConnectionString);

                return new ContextManager(optionsBuilder.Options);
            });

            services.AddSingleton<IRedisService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("Redis")!;
                return new RedisService(connectionString);
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
