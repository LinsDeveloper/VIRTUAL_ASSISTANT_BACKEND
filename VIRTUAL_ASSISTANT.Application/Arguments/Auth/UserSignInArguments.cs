using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Application.Arguments.Auth
{
    public class UserSignInArguments
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public required string Password { get; set; }
    }
}
