using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;

namespace Repository
{
    public interface IEditoraRepository : IRepositoryBase<Editora>, IDisposable
    {
        IEnumerable<EditoraViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "");
    }
}