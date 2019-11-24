using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IPlanoEnsinoRepository : IRepositoryBase<PlanoEnsino>, IDisposable
    {
        IEnumerable<PlanoEnsinoViewModel> Grid(string Buscar, int? TurmaId = null, int? DiciplinaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");

        IEnumerable<PlanoEnsinoViewModel> Report();
    }
}