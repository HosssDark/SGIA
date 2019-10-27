using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ILivroRepository : IRepositoryBase<Livro>, IDisposable
    {
        IEnumerable<LivroViewModel> Grid(string Buscar, int? StatusId = null, int? EditoraId = null, DateTime? DataInicial = null, DateTime? DataFinal = null);
    }
}