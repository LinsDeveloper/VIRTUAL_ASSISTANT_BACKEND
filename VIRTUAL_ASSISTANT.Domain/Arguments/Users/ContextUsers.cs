using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.Arguments.Users
{
    public class ContextUsers
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
    }
}
