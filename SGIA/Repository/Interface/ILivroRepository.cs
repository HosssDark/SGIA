using Domain;
using System;

namespace Repository
{
    public interface ILivroRepository : IRepositoryBase<Livro>, IDisposable
    {
    }
}