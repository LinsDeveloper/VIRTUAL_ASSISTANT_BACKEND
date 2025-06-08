using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.DTO
{
    public class ConfigRabbitMQ
    {
        public required string Host { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
