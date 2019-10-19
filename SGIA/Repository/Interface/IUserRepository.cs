using Domain;
using System;

namespace Repository
{
    public interface IUserRepository : IRepositoryBase<User>, IDisposable
    {
        User VerificationEmail(string Email);
    }
}