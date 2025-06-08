using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Arguments.Users;

namespace VIRTUAL_ASSISTANT.Application.Helpers
{
    public class ClaimsHelpers
    {
        public static int GetUserId(string? userIdStr)
        {
            int userId;
            if (!int.TryParse(userIdStr, out userId))
            {
                throw new ArgumentNullException("userId inválido");
            }

            return userId;
        }

        public static int GetCustomerId(string? customerIdStr)
        {
            int customerId;
            if (!int.TryParse(customerIdStr, out customerId))
            {
                throw new ArgumentNullException("customerId inválido");
            }

            return customerId;
        }

        public static ContextUsers GetUserFromContext(HttpContext httpContext)
        {
            var context = new ContextUsers()
            {
                UserId = GetUserId(httpContext.Items["userId"]?.ToString()),
                Name = httpContext.Items["unique_name"]?.ToString() ?? string.Empty,
            };

            return context;
        }
    }
}
