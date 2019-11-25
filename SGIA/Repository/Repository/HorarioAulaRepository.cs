using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class HorarioAulaRepository : RepositoryBase<HorarioAula>, IHorarioAulaRepository
    {
        public override HorarioAula Add(HorarioAula Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<HorarioAula> AddAll(List<HorarioAula> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<HorarioAulaViewModel> Grid(string Periodo = null, int? TurmaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IHorarioAulaRepository _horRep = new HorarioAulaRepository();
            IDiciplinaRepository _dicRep = new DiciplinaRepository();
            ITurmaRepository _turRep = new TurmaRepository();
            IStatusRepository _staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Diciplinas = _dicRep.GetAll();

            var Model = (from hr in this.GetAll()
                         join dp1 in Diciplinas on hr.DiciplinaPrimeiroId equals dp1.DiciplinaId into r1
                         from dp1 in r1.DefaultIfEmpty()
                         join dp2 in Diciplinas on hr.DiciplinaSegundoId equals dp2.DiciplinaId into r2
                         from dp2 in r2.DefaultIfEmpty()
                         join dp3 in Diciplinas on hr.DiciplinaTerceiroId equals dp3.DiciplinaId into r3
                         from dp3 in r3.DefaultIfEmpty()
                         join dp4 in Diciplinas on hr.DiciplinaQuartoId equals dp4.DiciplinaId into r4
                         from dp4 in r4.DefaultIfEmpty()
                         join tm in _turRep.GetAll() on hr.TurmaId equals tm.TurmaId 
                         join sta in _staRep.GetAll() on hr.StatusId equals sta.StatusId
                         select new HorarioAulaViewModel
                         {
                             HorarioAulaId = hr.HorarioAulaId,
                             DiciplinaPrimeiroId = hr.DiciplinaPrimeiroId,
                             DiciplinaPrimeiro = dp1 != null ? dp1.Nome : "",
                             DiciplinaSegundoId = hr.DiciplinaSegundoId,
                             DiciplinaSegundo = dp2 != null ? dp2.Nome : "",
                             DiciplinaTerceiroId = hr.DiciplinaTerceiroId,
                             DiciplinaTerceiro = dp3 != null ? dp3.Nome : "",
                             DiciplinaQuartoId = hr.DiciplinaQuartoId,
                             DiciplinaQuarto = dp4 != null ? dp4.Nome : "",
                             DataCadastro = hr.DataCadastro,
                             DiaSemana = hr.DiaSemana,
                             Periodo = hr.Periodo,
                             StatusId = hr.StatusId,
                             Status = sta.Descricao,
                             TurmaId = hr.TurmaId,
                             Turma = tm.Nome,
                             StatusIcon = sta.Icon,
                             Image = paramRep.GetImage(hr.HorarioAulaId, "images", "HorarioAulas", "HorarioAula", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Periodo))
                Model = Model.Where(a => a.Periodo == Periodo);

            if (TurmaId != null)
                Model = Model.Where(a => a.TurmaId == TurmaId);

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<HorarioAulaViewModel> Report()
        {
            IHorarioAulaRepository _horRep = new HorarioAulaRepository();
            IDiciplinaRepository _dicRep = new DiciplinaRepository();
            ITurmaRepository _turRep = new TurmaRepository();
            IStatusRepository _staRep = new StatusRepository();

            var Diciplinas = _dicRep.GetAll();

            return (from hr in this.GetAll()
                    join dp1 in Diciplinas on hr.DiciplinaPrimeiroId equals dp1.DiciplinaId into r1
                    from dp1 in r1.DefaultIfEmpty()
                    join dp2 in Diciplinas on hr.DiciplinaSegundoId equals dp2.DiciplinaId into r2
                    from dp2 in r2.DefaultIfEmpty()
                    join dp3 in Diciplinas on hr.DiciplinaTerceiroId equals dp3.DiciplinaId into r3
                    from dp3 in r3.DefaultIfEmpty()
                    join dp4 in Diciplinas on hr.DiciplinaQuartoId equals dp4.DiciplinaId into r4
                    from dp4 in r4.DefaultIfEmpty()
                    join tm in _turRep.GetAll() on hr.HorarioAulaId equals tm.TurmaId into r5
                    from tm in r5.DefaultIfEmpty()
                    join sta in _staRep.GetAll() on hr.StatusId equals sta.StatusId
                    select new HorarioAulaViewModel
                    {
                        HorarioAulaId = hr.HorarioAulaId,
                        DiciplinaPrimeiroId = hr.DiciplinaPrimeiroId,
                        DiciplinaPrimeiro = dp1.Nome,
                        DiciplinaSegundoId = hr.DiciplinaSegundoId,
                        DiciplinaSegundo = dp2.Nome,
                        DiciplinaTerceiroId = hr.DiciplinaTerceiroId,
                        DiciplinaTerceiro = dp3.Nome,
                        DiciplinaQuartoId = hr.DiciplinaQuartoId,
                        DiciplinaQuarto = dp4.Nome,
                        DataCadastro = hr.DataCadastro,
                        DiaSemana = hr.DiaSemana,
                        Periodo = hr.Periodo,
                        StatusId = hr.StatusId,
                        Status = sta.Descricao,
                        TurmaId = hr.TurmaId,
                        Turma = tm != null ? tm.Nome : ""
                    });
        }
    }
}