using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IPlanoTrabalhoRepository : IRepositoryBase<PlanoTrabalho>, IDisposable
    {
        IEnumerable<PlanoTrabalhoViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");

        IEnumerable<PlanoTrabalhoViewModel> Report();
    }
}