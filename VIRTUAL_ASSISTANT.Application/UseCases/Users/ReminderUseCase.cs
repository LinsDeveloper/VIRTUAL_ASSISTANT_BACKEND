using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Application.Interfaces.Providers;
using VIRTUAL_ASSISTANT.Application.Interfaces.UseCases;
using VIRTUAL_ASSISTANT.Domain.Constants;
using VIRTUAL_ASSISTANT.Domain.DTO.Notifications;
using VIRTUAL_ASSISTANT.Domain.Entities.Integrations.Reminders;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;
using VIRTUAL_ASSISTANT.Domain.Interfaces;

namespace VIRTUAL_ASSISTANT.Application.UseCases.Users
{
    public class ReminderUseCase : IReminderUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientService _httpClientService;
        private const string _expoTokenTemporary = "ExponentPushToken[zUPz5VPfGRitcVwPBqCktd]";
        private readonly IUserContextProvider _userContextProvider;

        public ReminderUseCase(IUnitOfWork unitOfWork, IHttpClientService httpClientService, IUserContextProvider userContextProvider)
        {
            _unitOfWork = unitOfWork;
            _httpClientService = httpClientService;
            _userContextProvider = userContextProvider;
        }

        public async Task<Result<string, string>> ReminderRegister(UserReminders userReminders)
        {
            var userContext = _userContextProvider.GetCurrentUser();
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(x => x.UserId == userContext.UserId);
            if (user == null)
                return Result<string, string>.NotFoundResult("Usuário não encontrado!");

            userReminders.User = user;

            await _unitOfWork.Repository<UserReminders>().CreateAsync(userReminders);
            await _unitOfWork.CommitAsync();

            return Result<string, string>.SuccessResult("Lembrete agendado com sucesso!");
        }

        public async Task ReminderProcess(CancellationToken cancellationToken)
        {
            var reminderPending = await _unitOfWork.Repository<UserReminders>().FindAsync(x => x.ReminderTimer < DateTime.UtcNow);

            foreach (var reminder in reminderPending)
            {
                var notify = new ReminderNotificationDTO()
                {
                    To = _expoTokenTemporary,
                    Sound = "default",
                    Title = reminder.Title,
                    Body = reminder.Message,
                    Data = new Data { Tipo = "default"}
                };

                await _httpClientService.PostAsync<ReminderNotificationDTO, ReminderNotificationResponseDTO>(ConstantsHttpClientServices.EXPO, "/--/api/v2/push/send", notify);
                _unitOfWork.Repository<UserReminders>().Delete(reminder);
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
