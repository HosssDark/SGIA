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

        public IEnumerable<DicenteViewModel> Grid(string Buscar, int? StatusId = null, int? Matricula = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            IStatusRepository _staRep = new StatusRepository();

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
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (Matricula != null)
                Model = Model.Where(a => a.Matricula == Matricula);

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