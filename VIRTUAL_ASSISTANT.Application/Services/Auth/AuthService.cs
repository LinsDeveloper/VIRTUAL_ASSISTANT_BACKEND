using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Application.DTO.Auth;
using VIRTUAL_ASSISTANT.Application.Helpers;
using VIRTUAL_ASSISTANT.Application.Interfaces.Auth;
using VIRTUAL_ASSISTANT.Application.Interfaces.Providers;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;
using VIRTUAL_ASSISTANT.Domain.Enum;
using VIRTUAL_ASSISTANT.Domain.Interfaces;
using VIRTUAL_ASSISTANT.Domain.Interfaces.Cache;

namespace VIRTUAL_ASSISTANT.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IRedisService _redisService;
        private readonly IUserContextProvider _userContextProvider;

        public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IRedisService redisService, IUserContextProvider userContextProvider)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _redisService = redisService;
            _userContextProvider = userContextProvider;
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

        public async Task<Result<string, string>> IntegrationSignIn(IntegrationSignInDTO integrationSignInDTO)
        {
            var userContext = _userContextProvider.GetCurrentUser();
            switch (integrationSignInDTO.IntegrationType)
            {
                case IntegrationTypeEnum.SPOTIFY:
                    await AttachSpotify(integrationSignInDTO.Token, userContext.UserId);
                    break;
                case IntegrationTypeEnum.GMAIL:
                    await AttachGmail(integrationSignInDTO.Token, userContext.UserId);
                    break;
            }

            return Result<string, string>.SuccessResult(integrationSignInDTO.IntegrationType == IntegrationTypeEnum.SPOTIFY ? "Spotify configurado com sucesso!" : "Gmail configurado com sucesso!");
        }

        public async Task AttachSpotify(string token, int userId)
        {
            await _redisService.SetAsync($"SPOTIFY-{userId}-{token}", token, TimeSpan.FromMinutes(60));
        }

        public async Task AttachGmail(string token, int userId)
        {
            await _redisService.SetAsync($"GMAIL-{userId}-{token}", token, TimeSpan.FromMinutes(60));
        }
    }
}
