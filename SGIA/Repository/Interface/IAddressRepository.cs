using Domain;
using System;

namespace Repository
{
    public interface IAddressRepository : IRepositoryBase<Address>, IDisposable
    {
    }
}