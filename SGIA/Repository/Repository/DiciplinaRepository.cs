using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Repository.Repository.ViewModel;

namespace Repository
{
    public class DiciplinaRepository : RepositoryBase<Diciplina>, IDiciplinaRepository
    {
        public override Diciplina Add(Diciplina Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Diciplina> AddAll(List<Diciplina> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<DiciplinaViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            IStatusRepository _staRep = new StatusRepository();

            var Model = (from dp in this.GetAll()
                         join sta in _staRep.GetAll() on dp.StatusId equals sta.StatusId
                         select new DiciplinaViewModel
                         {
                             DiciplinaId = dp.DiciplinaId,
                             TurmaId = dp.TurmaId,
                             StatusId = dp.StatusId,
                             CargaHoraria = dp.CargaHoraria,
                             CreditoAtividadeCampo = dp.CreditoAtividadeCampo,
                             CreditoAtividadePratica = dp.CreditoAtividadePratica,
                             CreditoEnsino = dp.CreditoEnsino,
                             Ementa = dp.Ementa,
                             HoraSemanal = dp.HoraSemanal,
                             Nome = dp.Nome,
                             DataCadastro = dp.DataCadastro,
                             Status = sta.Descricao,
                         });

            #region + Filters

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

        public IEnumerable<DiciplinaViewModel> Report()
        {
            IStatusRepository _staRep = new StatusRepository();

            return (from dp in this.GetAll()
                    join sta in _staRep.GetAll() on dp.StatusId equals sta.StatusId
                    select new DiciplinaViewModel
                    {
                        DiciplinaId = dp.DiciplinaId,
                        TurmaId = dp.TurmaId,
                        StatusId = dp.StatusId,
                        CargaHoraria = dp.CargaHoraria,
                        CreditoAtividadeCampo = dp.CreditoAtividadeCampo,
                        CreditoAtividadePratica = dp.CreditoAtividadePratica,
                        CreditoEnsino = dp.CreditoEnsino,
                        Ementa = dp.Ementa,
                        HoraSemanal = dp.HoraSemanal,
                        Nome = dp.Nome,
                        DataCadastro = dp.DataCadastro,
                        Status = sta.Descricao,
                    });
        }
    }
}