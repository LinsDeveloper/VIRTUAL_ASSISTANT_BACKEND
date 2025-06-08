using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Application.DTO.Auth
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string? TokenType { get; set; }
        public DateTime Expiration { get; set; }

        public TokenDTO(string token, string tokenType, DateTime expiration)
        {
            Token = token;
            TokenType = tokenType;
            Expiration = expiration;
        }
    }
}
