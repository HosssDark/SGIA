using Domain;
using System;

namespace Repository
{
    public interface IUserImageRepository : IRepositoryBase<UserImage>, IDisposable
    {
    }
}