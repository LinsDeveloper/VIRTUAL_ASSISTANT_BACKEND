using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Application.Helpers;
using VIRTUAL_ASSISTANT.Application.Interfaces.Auth;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;
using VIRTUAL_ASSISTANT.Domain.Interfaces;

namespace VIRTUAL_ASSISTANT.Application.Services.Auth
{
    public class AuthService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<Result<UserSignInResponseArguments, string>> SignIn(UserSignInArguments signIn)
        {
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(x => x.Email == signIn.Email, asNoTracking: true);
            if (user == null)
                return Result<UserSignInResponseArguments, string>.UnauthorizedResult("Email incorreto!");

            var passwordHash = ApplyHash.HashPassword(signIn.Password, user.SaltKey);

            if (passwordHash != null && passwordHash != user.Password)
                return Result<UserSignInResponseArguments, string>.UnauthorizedResult("Senha incorreto!");

            var newAccessToken = _jwtService.GenerateAcessToken(user);

            var response = new UserSignInResponseArguments()
            {
                Name = user.Name,
                Email = user.Email,
                AccessToken = newAccessToken.Token,
                ExpirationAccess = newAccessToken.Expiration,
            };

            return Result<UserSignInResponseArguments, string>.SuccessResult(response);
        }
    }
}
