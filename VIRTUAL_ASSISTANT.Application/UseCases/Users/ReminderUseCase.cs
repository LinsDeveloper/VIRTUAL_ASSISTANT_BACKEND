using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Interfaces.UseCases;
using VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders;
using VIRTUAL_ASSISTANT.Domain.Interfaces;

namespace VIRTUAL_ASSISTANT.Application.UseCases.Users
{
    public class ReminderUseCase : IReminderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientService _httpClientService;

        public ReminderUseCase(IUnitOfWork unitOfWork, IHttpClientService httpClientService)
        {
            _unitOfWork = unitOfWork;
            _httpClientService = httpClientService;
        }

        public async Task ReminderProcess()
        {
            var reminderPending = await _unitOfWork.Repository<UserReminders>().FindAsync(x => x.ReminderTimer > DateTime.Now);

            foreach (var reminder in reminderPending)
            {

            }
        }
    }
}
