using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.DTO.Notifications
{
    public class ReminderNotificationDTO
    {
        public required string To { get; set; }
        public required string Sound { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
        public required Data Data { get; set; }
    }

    public class Data
    {
        public required string Tipo { get; set; }
    }
}
