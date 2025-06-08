using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.DTO.Auth;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;

namespace VIRTUAL_ASSISTANT.Application.Interfaces.Auth
{
    public interface IJwtService
    {
        public TokenDTO GenerateAcessToken(User user);
    }
}
