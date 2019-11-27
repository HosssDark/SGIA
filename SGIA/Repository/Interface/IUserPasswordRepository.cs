using Domain;
using System;

namespace Repository
{
    public interface IUserPasswordRepository: IRepositoryBase<UserPassword>, IDisposable
    {
        UserPassword VerificationPassword(string Email, string Password);

        string UserRegister(string Name, string Email, string Password);

        void ChangePassword(int UserId, string Password);

        void ChangePassword(string Guid, string Email, string Password);

        string ChangeGuid(string Email);
    }
}