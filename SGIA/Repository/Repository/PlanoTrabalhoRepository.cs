using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class PlanoTrabalhoRepository : RepositoryBase<PlanoTrabalho>, IPlanoTrabalhoRepository
    {
        public override PlanoTrabalho Add(PlanoTrabalho Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<PlanoTrabalho> AddAll(List<PlanoTrabalho> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<PlanoTrabalhoViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            IUserRepository _userRep = new UserRepository();
            IStatusRepository _staRep = new StatusRepository();

            var Model = (from pl in this.GetAll()
                         join use in _userRep.GetAll() on pl.UserId equals use.UserId
                         join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                         select new PlanoTrabalhoViewModel
                         {
                             PlanoTrabalhoId = pl.PlanoTrabalhoId,
                             UserId = pl.UserId,
                             StatusId = pl.StatusId,
                             DataCadastro = pl.DataCadastro,
                             DescricaoAtividade = pl.DescricaoAtividade,
                             DiaSemana = pl.DiaSemana,
                             Docente = use.Nome,
                             HoraEncerramento = pl.HoraEncerramento,
                             HoraInicio = pl.HoraInicio,
                             Status = sta.Descricao
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Docente.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<PlanoTrabalhoViewModel> Report()
        {
            IUserRepository _userRep = new UserRepository();
            IStatusRepository _staRep = new StatusRepository();

            return (from pl in this.GetAll()
                         join use in _userRep.GetAll() on pl.UserId equals use.UserId
                         join sta in _staRep.GetAll() on pl.StatusId equals sta.StatusId
                         select new PlanoTrabalhoViewModel
                         {
                             PlanoTrabalhoId = pl.PlanoTrabalhoId,
                             UserId = pl.UserId,
                             StatusId = pl.StatusId,
                             DataCadastro = pl.DataCadastro,
                             DescricaoAtividade = pl.DescricaoAtividade,
                             DiaSemana = pl.DiaSemana,
                             Docente = use.Nome,
                             HoraEncerramento = pl.HoraEncerramento,
                             HoraInicio = pl.HoraInicio,
                             Status = sta.Descricao
                         });
        }
    }
}