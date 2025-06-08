using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Helpers;
using VIRTUAL_ASSISTANT.Application.Interfaces.Providers;
using VIRTUAL_ASSISTANT.Domain.Arguments.Users;

namespace VIRTUAL_ASSISTANT.Application.Services.Providers
{
    public class HttpUserContextProvider : IUserContextProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpUserContextProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ContextUsers GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("httpContext cannot be null");
            }

            return ClaimsHelpers.GetUserFromContext(httpContext);
        }

        public ContextUsers GetCurrentUserMocked()
        {
            var userContext = new ContextUsers
            {
                UserId = 2,
                Name = "Pedro Henrique Valverde Lins",
            };

            return userContext;
        }
    }
}
