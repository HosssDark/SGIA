using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IProjetoRepository : IRepositoryBase<Projeto>, IDisposable
    {
        IEnumerable<ProjetoViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null);

        IEnumerable<ProjetoViewModel> Report();
    }
}