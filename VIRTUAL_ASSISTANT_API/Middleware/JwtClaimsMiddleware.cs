using System.IdentityModel.Tokens.Jwt;

namespace VIRTUAL_ASSISTANT.API.Middleware
{
    public class JwtClaimsMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    var userName = jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name")?.Value;
                    var email = jwtToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
                    var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;

                    context.Items["userName"] = userName;
                    context.Items["email"] = email;
                    context.Items["userId"] = userId;
                    context.Items["name"] = userName;
                }
                catch (Exception)
                {
                }
            }

            await _next(context);
        }
    }
}
