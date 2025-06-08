using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;

namespace VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders
{
    public class UserReminders
    {
        public Guid ReminderId { get; set; }
        public required User User { get; set; }
        public required string Message {  get; set; }
        public DateTime ReminderTimer { get; set; }
    }
}
