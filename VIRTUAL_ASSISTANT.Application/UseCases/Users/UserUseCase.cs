using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Application.Common;
using VIRTUAL_ASSISTANT.Application.Helpers;
using VIRTUAL_ASSISTANT.Application.Interfaces.UseCases;
using VIRTUAL_ASSISTANT.Domain.Arguments.Users;
using VIRTUAL_ASSISTANT.Domain.Entities.Users;
using VIRTUAL_ASSISTANT.Domain.Interfaces;

namespace VIRTUAL_ASSISTANT.Application.UseCases.Users
{
    public class UserUseCase: IUseCases
    {
        private readonly IUnitOfWork _unitOfWork;
        private const string MENSSAGE_OBJECT_NULL = "OBJECT_NULL";

        public UserUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<string, string>> UserRegister(UserRegisterArguments userRegisterArguments)
        {
            var user = _unitOfWork.Repository<User>().FirstOrDefaultAsync(x => x.Email == userRegisterArguments.Email);
            if (user != null)
                return Result<string, string>.BadRequestResult("Usuário já está cadastrado no sistema");

            var (salt, passwordHash, passwordGenerated) = GenerateSaltAndPasswordToUser();

            var newUser = new User()
            {
                Email = userRegisterArguments.Email,
                Name = userRegisterArguments.Name,
                Address = userRegisterArguments.Address,
                PhoneNumber = userRegisterArguments.PhoneNumber,
                SaltKey = salt,
                Password = passwordHash
            };

            await _unitOfWork.Repository<User>().CreateAsync(newUser);
            await _unitOfWork.CommitAsync();

            return Result<string, string>.SuccessResult($"Sua senha é: {passwordGenerated}");
        }

        /// <summary>
        /// Método responsável por gerar salt e password para o usuário.
        /// </summary>
        /// <returns>Irá retornar o Salt e o Password gerados.</returns>
        public static (string, string, string) GenerateSaltAndPasswordToUser()
        {
            var salt = ApplyHash.GenerateSalt();
            var passwordGenerated = ApplyHash.GenerateRandomPassword();
            var passwordHash = ApplyHash.HashPassword(passwordGenerated, salt);

            return (salt, passwordHash, passwordGenerated);
        }
    }
}

