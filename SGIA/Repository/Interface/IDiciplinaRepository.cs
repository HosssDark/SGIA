using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IDiciplinaRepository : IRepositoryBase<Diciplina>, IDisposable
    {
        IEnumerable<DiciplinaViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");

        IEnumerable<DiciplinaViewModel> Report();
    }
}