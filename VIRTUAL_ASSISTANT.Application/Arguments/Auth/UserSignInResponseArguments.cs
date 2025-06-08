using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Application.Arguments.Auth
{
    public class UserSignInResponseArguments
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string AccessToken { get; set; }
        public DateTime ExpirationAccess { get; set; }
    }
}
