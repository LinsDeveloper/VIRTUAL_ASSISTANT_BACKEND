using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.DTO;

namespace VIRTUAL_ASSISTANT.Infra.Broker
{
    public class RabbitMqConnectionService : IDisposable
    {
        public IConnection Connection { get; }
        public IChannel Channel { get; }

        public RabbitMqConnectionService(IOptions<ConfigRabbitMQ> rabbitMqSettings, ILogger<RabbitMqConnectionService> logger)
        {
            var settings = rabbitMqSettings.Value;

            var factory = new ConnectionFactory
            {
                HostName = settings.Host,
                UserName = settings.Username,
                Password = settings.Password
            };

            Connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            Channel = Connection.CreateChannelAsync().GetAwaiter().GetResult();

            logger.LogInformation("Conexão e canal do RabbitMQ criados com sucesso.");
        }

        public void Dispose()
        {
            Channel?.CloseAsync();
            Connection?.CloseAsync();
        }

    }
}
