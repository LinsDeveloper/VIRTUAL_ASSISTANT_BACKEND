using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Arguments.Auth;
using VIRTUAL_ASSISTANT.Application.DTO.Auth;
using VIRTUAL_ASSISTANT.Application.Interfaces.Auth;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;

namespace VIRTUAL_ASSISTANT.Application.Services.Auth
{
    public class JwtService: IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public TokenDTO GenerateAcessToken(User user)
        {
            var identityClaims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            });

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey)),
                    SecurityAlgorithms.HmacSha256),
                Subject = identityClaims,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(_jwtOptions.ExpirationAccessToken),
                IssuedAt = DateTime.UtcNow,
                TokenType = "at+jwt"
            });

            var encodedJwt = handler.WriteToken(securityToken);
            var tokenDTO = new TokenDTO(encodedJwt, "ACCESS_TOKEN", DateTime.UtcNow.AddSeconds(_jwtOptions.ExpirationAccessToken));
            return tokenDTO;
        }
    }
}
