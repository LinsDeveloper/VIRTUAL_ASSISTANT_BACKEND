using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.Arguments.Configurations
{
    public class HttpClientConfig
    {
        public required string BaseAddress { get; set; }
        public int TimeoutSeconds { get; set; }

    }
}
