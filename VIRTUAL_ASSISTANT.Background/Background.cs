using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using VIRTUAL_ASSISTANT.Domain.DTO;

namespace VIRTUAL_ASSISTANT.Background
{
    public class Background: IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Background> _logger;

        private Timer? _timer;
        private readonly TimeSpan _outboxInterval = TimeSpan.FromSeconds(20);
        private readonly TimeSpan _notificationInterval = TimeSpan.FromSeconds(25);
        private readonly ConfigRabbitMQ _configRabbitMQ; 

        public Background(IServiceProvider serviceProvider, IServiceScopeFactory serviceScopeFactory, ILogger<Background> logger, , IOptions<ConfigRabbitMQ> rabbitMqSettings)
        {
            _serviceProvider = serviceProvider;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _configRabbitMQ = rabbitMqSettings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker started.");
            _timer = new Timer(ExecuteProcessing!, cancellationToken, TimeSpan.Zero, _outboxInterval);
            return Task.CompletedTask;
        }

        private async void ExecuteProcessing(object state)
        {
            var cancellationToken = (CancellationToken)state;
            _logger.LogInformation("Executing processing cycle.");

        }

        public async Task ProcessNotifications(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Processing outbox messages.");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<IOutboxPublisher>();
                    var messagesToPublish = await service.GetMessagesToPublishAsync(cancellationToken);

                    if (messagesToPublish.Any())
                    {
                        _logger.LogInformation("{Count} messages found in outbox.", messagesToPublish.Count());
                        foreach (var message in messagesToPublish)
                        {
                            await service.ProcessOutboxAsync(message, cancellationToken);
                            _logger.LogInformation("Processed message {MessageId}.", message.PersistenceId);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No messages found in outbox.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing outbox messages.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Worker disposed.");
            _timer?.Dispose();
        }
    }
}
