using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Enum;

namespace VIRTUAL_ASSISTANT.Application.DTO.Auth
{
    public class IntegrationSignInDTO
    {
        public IntegrationTypeEnum IntegrationType { get; set; }
        public required string Token { get; set; }
    }
}
