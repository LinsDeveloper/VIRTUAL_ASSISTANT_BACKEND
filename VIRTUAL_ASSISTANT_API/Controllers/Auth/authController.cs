using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.Application.Interfaces.Auth;

namespace VIRTUAL_ASSISTANT.API.Controllers.Auth
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtOptions _jwtOptions;
        public authController(IAuthService authService, IOptions<JwtOptions> jwtOptions)
        {
            _authService = authService;
            _jwtOptions = jwtOptions.Value;
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

        [HttpPost("spotify-sign")]
        public async Task<IActionResult> SpotifySignIn()
        {

        }
    }
}
