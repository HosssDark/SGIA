using Domain;
using System;

namespace Repository
{
    public interface IProjetoRepository : IRepositoryBase<Projeto>, IDisposable
    {
    }
}