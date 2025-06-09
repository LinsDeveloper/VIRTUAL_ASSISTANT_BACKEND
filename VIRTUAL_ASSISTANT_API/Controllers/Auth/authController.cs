using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.Application.DTO.Auth;
using VIRTUAL_ASSISTANT.Application.Interfaces.Auth;
using VIRTUAL_ASSISTANT.Application.Interfaces.UseCases;
using VIRTUAL_ASSISTANT.Domain.Arguments.Users;

namespace VIRTUAL_ASSISTANT.API.Controllers.Auth
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUseCases _useCases;
        private readonly JwtOptions _jwtOptions;
        public authController(IAuthService authService, IOptions<JwtOptions> jwtOptions, IUseCases useCases)
        {
            _authService = authService;
            _jwtOptions = jwtOptions.Value;
            _useCases = useCases;
        }

        /// <summary>
        /// Efetua login.
        /// </summary>
        /// <param name="userLogin">Dados de entrada.</param>
        /// <returns>Mensagem de sucesso.</returns>
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(UserSignInArguments userLogin)
        {
            var result = await _authService.SignIn(userLogin);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> IntegrationSignIn(UserRegisterArguments userRegisterArguments)
        {
            var result = await _useCases.UserRegister(userRegisterArguments);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("integration-sign-in")]
        public async Task<IActionResult> IntegrationSignIn(IntegrationSignInDTO integrationSignInDTO)
        {
            var result = await _authService.IntegrationSignIn(integrationSignInDTO);
            return StatusCode(result.StatusCode, result);
        }
    }
}
