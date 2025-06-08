using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Domain.DTO.Notifications
{
    public class ReminderNotificationResponseDTO
    {
        public required DataDTO Data { get; set; }
    }

    public class DataDTO
    {
        public required string Status { get; set; }
        public required string Id { get; set; }
    }

}
