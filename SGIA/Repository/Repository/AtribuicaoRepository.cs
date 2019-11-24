using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AtribuicaoRepository : RepositoryBase<Atribuicao>, IAtribuicaoRepository
    {
        public override Atribuicao Add(Atribuicao Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Atribuicao> AddAll(List<Atribuicao> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<AtribuicaoViewModel> Grid(string Buscar, int? StatusId = null, int? DiciplinaId = null, int? ProjetoId = null, int? DocenteId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            IStatusRepository _staRep = new StatusRepository();
            IDiciplinaRepository _dicRep = new DiciplinaRepository();
            IProjetoRepository _proRep = new ProjetoRepository();
            IUserRepository _userRep = new UserRepository();

            var Model = (from at in this.GetAll()
                         join sta in _staRep.GetAll() on at.StatusId equals sta.StatusId
                         join dp in _dicRep.GetAll() on at.DiciplinaId equals dp.DiciplinaId
                         join pj in _proRep.GetAll() on at.ProjetoId equals pj.ProjetoId
                         join dc in _userRep.GetAll() on at.UserId equals dc.UserId
                         select new AtribuicaoViewModel
                         {
                             AtribuicaoId = at.AtribuicaoId,
                             DiciplinaId = at.DiciplinaId,
                             ProjetoId = at.ProjetoId,
                             DocenteId = at.UserId,
                             DataCadastro = at.DataCadastro,
                             Diciplina = dp.Nome,
                             Projeto = pj.Nome,
                             StatusId = at.StatusId,
                             Status = sta.Descricao,
                             Docente = dc.Nome
                         });

            #region + Filtro

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DiciplinaId != null)
                Model = Model.Where(a => a.DiciplinaId == DiciplinaId);

            if (ProjetoId != null)
                Model = Model.Where(a => a.ProjetoId == ProjetoId);

            if (DocenteId != null)
                Model = Model.Where(a => a.DocenteId == DocenteId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<AtribuicaoViewModel> Report()
        {
            IStatusRepository _staRep = new StatusRepository();
            IDiciplinaRepository _dicRep = new DiciplinaRepository();
            IProjetoRepository _proRep = new ProjetoRepository();
            IUserRepository _userRep = new UserRepository();

            return (from at in this.GetAll()
                    join sta in _staRep.GetAll() on at.StatusId equals sta.StatusId
                    join dp in _dicRep.GetAll() on at.DiciplinaId equals dp.DiciplinaId
                    join pj in _proRep.GetAll() on at.ProjetoId equals pj.ProjetoId
                    join dc in _userRep.GetAll() on at.UserId equals dc.UserId
                    select new AtribuicaoViewModel
                    {
                        AtribuicaoId = at.AtribuicaoId,
                        DiciplinaId = at.DiciplinaId,
                        ProjetoId = at.ProjetoId,
                        DocenteId = at.UserId,
                        DataCadastro = at.DataCadastro,
                        Diciplina = dp.Nome,
                        Projeto = pj.Nome,
                        StatusId = at.StatusId,
                        Status = sta.Descricao,
                        Docente = dc.Nome
                    }).ToList();
        }
    }
}