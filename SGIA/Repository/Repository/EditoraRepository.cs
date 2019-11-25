using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EditoraRepository : RepositoryBase<Editora>, IEditoraRepository
    {
        public override Editora Add(Editora Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Editora> AddAll(List<Editora> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<EditoraViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IStatusRepository _staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Model = (from ed in this.GetAll()
                         join sta in _staRep.GetAll() on ed.StatusId equals sta.StatusId
                         select new EditoraViewModel
                         {
                             EditoraId = ed.EditoraId,
                             Nome = ed.Nome,
                             StatusId = ed.StatusId,
                             Status = sta.Descricao,
                             StatusIcon = sta.Icon,
                             DataCadastro = ed.DataCadastro,
                             Image = paramRep.GetImage(ed.EditoraId, "images", "Editoras", "Editora", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }
    }
}