using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class DicenteRepository : RepositoryBase<Dicente>, IDicenteRepository
    {
        public override Dicente Add(Dicente Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Dicente> AddAll(List<Dicente> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<DicenteViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IStatusRepository _staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Model = (from dc in this.GetAll()
                         join sta in _staRep.GetAll() on dc.StatusId equals sta.StatusId
                         select new DicenteViewModel
                         {
                             DicenteId = dc.DicenteId,
                             Matricula = dc.Matricula,
                             Nome = dc.Nome,
                             Email = dc.Email,
                             Telefone = dc.Telefone,
                             Celular = dc.Celular,
                             DataCadastro = dc.DataCadastro,
                             StatusId = dc.StatusId,
                             Status = sta.Descricao,
                             StatusIcon = sta.Icon,
                             Image = paramRep.GetImageUser(dc.DicenteId, "images", "Dicentes", "Dicente", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()) || a.Matricula.ToString() == Buscar);

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<DicenteViewModel> Report()
        {
            IStatusRepository _staRep = new StatusRepository();

            return (from dc in this.GetAll()
                         join sta in _staRep.GetAll() on dc.StatusId equals sta.StatusId
                         select new DicenteViewModel
                         {
                             DicenteId = dc.DicenteId,
                             Matricula = dc.Matricula,
                             Nome = dc.Nome,
                             Email = dc.Email,
                             Telefone = dc.Telefone,
                             Celular = dc.Celular,
                             DataCadastro = dc.DataCadastro,
                             StatusId = dc.StatusId,
                             Status = sta.Descricao,
                         });
        }
    }
}