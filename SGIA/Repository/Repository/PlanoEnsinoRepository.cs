using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class PlanoEnsinoRepository : RepositoryBase<PlanoEnsino>, IPlanoEnsinoRepository
    {
        public override PlanoEnsino Add(PlanoEnsino Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<PlanoEnsino> AddAll(List<PlanoEnsino> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<PlanoEnsinoViewModel> Grid(string Buscar, int? TurmaId = null, int? DiciplinaId = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            IPlanoEnsinoRepository _ensRep = new PlanoEnsinoRepository();
            IDiciplinaRepository _dicRep = new DiciplinaRepository();
            IUserRepository _userRep = new UserRepository();
            ITurmaRepository _turRep = new TurmaRepository();
            IStatusRepository _staRep = new StatusRepository();

            var Model = (from pl in _ensRep.GetAll()
                         join dp in _dicRep.GetAll() on pl.DiciplinaId equals dp.DiciplinaId
                         join use in _userRep.GetAll() on pl.UserId equals use.UserId
                         join tm in _turRep.GetAll() on pl.TurmaId equals tm.TurmaId
                         join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                         select new PlanoEnsinoViewModel
                         {
                             PlanoEnsinoId = pl.PlanoEnsinoId,
                             BiografiaBasicaId = pl.BiografiaBasicaId,
                             TurmaId = pl.TurmaId,
                             UserId = pl.UserId,
                             DiciplinaId = pl.DiciplinaId,
                             StatusId = pl.StatusId,
                             BiografiaComplementarId = pl.BiografiaComplementarId,
                             EmentaId = pl.EmentaId,
                             AtividadeTrabalhada = pl.AtividadeTrabalhada,
                             DataCadastro = pl.DataCadastro,
                             DataEmissao = pl.DataEmissao,
                             Diciplina = dp.Nome,
                             Docente = use.Nome,
                             EspecificacaoConteudo = pl.EspecificacaoConteudo,
                             MetodologiaAvaliacao = pl.MetodologiaAvaliacao,
                             ObjetivoArea = pl.ObjetivoArea,
                             ObjetivoGeral = pl.ObjetivoGeral,
                             RecursoUtilizado = pl.RecursoUtilizado,
                             Status = sta.Descricao,
                             TecnicaPedagogica = pl.TecnicaPedagogica,
                             Turma = tm.Nome
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Docente.ToLower().Contains(Buscar.ToLower()));

            if (TurmaId != null)
                Model = Model.Where(a => a.TurmaId == TurmaId);

            if (DiciplinaId != null)
                Model = Model.Where(a => a.DiciplinaId == DiciplinaId);

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<PlanoEnsinoViewModel> Report()
        {
            IPlanoEnsinoRepository _ensRep = new PlanoEnsinoRepository();
            IDiciplinaRepository _dicRep = new DiciplinaRepository();
            IUserRepository _userRep = new UserRepository();
            ITurmaRepository _turRep = new TurmaRepository();
            IStatusRepository _staRep = new StatusRepository();

            return (from pl in _ensRep.GetAll()
                         join dp in _dicRep.GetAll() on pl.DiciplinaId equals dp.DiciplinaId
                         join use in _userRep.GetAll() on pl.UserId equals use.UserId
                         join tm in _turRep.GetAll() on pl.TurmaId equals tm.TurmaId
                         join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                         select new PlanoEnsinoViewModel
                         {
                             PlanoEnsinoId = pl.PlanoEnsinoId,
                             BiografiaBasicaId = pl.BiografiaBasicaId,
                             TurmaId = pl.TurmaId,
                             UserId = pl.UserId,
                             DiciplinaId = pl.DiciplinaId,
                             StatusId = pl.StatusId,
                             BiografiaComplementarId = pl.BiografiaComplementarId,
                             EmentaId = pl.EmentaId,
                             AtividadeTrabalhada = pl.AtividadeTrabalhada,
                             DataCadastro = pl.DataCadastro,
                             DataEmissao = pl.DataEmissao,
                             Diciplina = dp.Nome,
                             Docente = use.Nome,
                             EspecificacaoConteudo = pl.EspecificacaoConteudo,
                             MetodologiaAvaliacao = pl.MetodologiaAvaliacao,
                             ObjetivoArea = pl.ObjetivoArea,
                             ObjetivoGeral = pl.ObjetivoGeral,
                             RecursoUtilizado = pl.RecursoUtilizado,
                             Status = sta.Descricao,
                             TecnicaPedagogica = pl.TecnicaPedagogica,
                             Turma = tm.Nome
                         });
        }
    }
}