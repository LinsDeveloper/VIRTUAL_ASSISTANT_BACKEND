using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Application.DTO.Auth;

namespace VIRTUAL_ASSISTANT.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<Result<UserSignInResponseArguments, string>> SignIn(UserSignInArguments signIn);
        Task<Result<string, string>> IntegrationSignIn(IntegrationSignInDTO integrationSignInDTO);
    }
}
