using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;

namespace VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders
{
    public class UserReminders
    {
        public Guid ReminderId { get; set; }
        public User? User { get; set; }
        public required string Title { get; set; }
        public required string Message {  get; set; }
        [JsonPropertyName("reminder_timer")]
        public DateTime ReminderTimer { get; set; }
    }
}
