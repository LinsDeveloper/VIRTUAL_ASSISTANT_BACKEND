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
                    var customerId = jwtToken.Claims.FirstOrDefault(x => x.Type == "customerId")?.Value;
                    var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid")?.Value;
                    var name = jwtToken.Claims.FirstOrDefault(x => x.Type == "name")?.Value;

                    context.Items["userName"] = userName;
                    context.Items["email"] = email;
                    context.Items["customerId"] = customerId;
                    context.Items["userId"] = userId;
                    context.Items["name"] = name;
                }
                catch (Exception)
                {
                }
            }

            await _next(context);
        }
    }
}
