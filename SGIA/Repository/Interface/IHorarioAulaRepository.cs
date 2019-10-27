using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IHorarioAulaRepository : IRepositoryBase<HorarioAula>, IDisposable
    {
        IEnumerable<HorarioAulaViewModel> Grid(string Periodo = null, int? TurmaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null);

        IEnumerable<HorarioAulaViewModel> Report();
    }
}