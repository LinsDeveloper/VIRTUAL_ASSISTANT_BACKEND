using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Domain.Arguments.Users;

namespace VIRTUAL_ASSISTANT.Application.Interfaces.UseCases
{
    public interface IUseCases
    {
        Task<Result<string, string>> UserRegister(UserRegisterArguments userRegisterArguments);
    }
}
