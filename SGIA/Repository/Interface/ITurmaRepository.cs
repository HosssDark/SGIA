using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface ITurmaRepository : IRepositoryBase<Turma>, IDisposable
    {
        IEnumerable<TurmaViewModel> Grid(string Buscar = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");

        IEnumerable<TurmaViewModel> Report();
    }
}