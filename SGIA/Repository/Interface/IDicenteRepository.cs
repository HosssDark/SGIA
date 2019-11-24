using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IDicenteRepository : IRepositoryBase<Dicente>, IDisposable
    {
        IEnumerable<DicenteViewModel> Grid(string Buscar, int? StatusId = null, int? Matricula = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");

        IEnumerable<DicenteViewModel> Report();
    }
}