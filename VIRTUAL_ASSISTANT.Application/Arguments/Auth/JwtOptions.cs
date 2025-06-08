using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Application.Arguments.Auth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpirationAccessToken { get; set; }
        public int ExpirationRefreshToken { get; set; }
    }
}
