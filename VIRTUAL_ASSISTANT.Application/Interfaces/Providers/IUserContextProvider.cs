using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Arguments.Users;

namespace VIRTUAL_ASSISTANT.Application.Interfaces.Providers
{
    public interface IUserContextProvider
    {
        public ContextUsers GetCurrentUser();
        public ContextUsers GetCurrentUserMocked();
    }
}
