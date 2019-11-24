using Domain;
using System;

namespace Repository
{
    public interface IUserPasswordRepository: IRepositoryBase<UserPassword>, IDisposable
    {
        UserPassword VerificationPassword(string Password);

        string UserRegister(string Email, string Password);
    }
}