using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders;

namespace VIRTUAL_ASSISTANT.Application.Interfaces.UseCases
{
    public interface IReminderUseCase
    {
        Task<Result<string, string>> ReminderRegister(UserReminders userReminders);
        Task ReminderProcess(CancellationToken cancellationToken);
    }
}
