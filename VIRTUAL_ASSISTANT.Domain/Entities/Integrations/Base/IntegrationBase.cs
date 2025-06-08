using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Base
{
    public class IntegrationBase
    {
        public int IntegrationId { get; set; }
        public string IntegrationName { get; set; }
        public string IntegrationProvider { get; set; }

    }
}
