using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IAtribuicaoRepository : IRepositoryBase<Atribuicao>, IDisposable
    {
        IEnumerable<AtribuicaoViewModel> Grid(string Buscar, int? StatusId = null, int? DiciplinaId = null, int? ProjetoId = null, int? DocenteId = null, DateTime? DataInicial = null, DateTime? DataFinal = null);

        IEnumerable<AtribuicaoViewModel> Report();
    }
}