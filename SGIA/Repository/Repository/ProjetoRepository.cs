using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProjetoRepository : RepositoryBase<Projeto>, IProjetoRepository
    {
        public override Projeto Add(Projeto Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Projeto> AddAll(List<Projeto> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<ProjetoViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IUserRepository _userRep = new UserRepository();
            IStatusRepository _staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Model = (from pj in this.GetAll()
                         join use in _userRep.GetAll() on pj.UserId equals use.UserId
                         join sta in _staRep.GetAll() on pj.StatusId equals sta.StatusId
                         select new ProjetoViewModel
                         {
                             ProjetoId = pj.ProjetoId,
                             UserId = pj.UserId,
                             StatusId = pj.StatusId,
                             Docente = use.Nome,
                             Nome = pj.Nome,
                             Status = sta.Descricao,
                             CargaHoraria = pj.CargaHoraria,
                             DataCadastro = pj.DataCadastro,
                             DataInicio = pj.DataInicio,
                             DataTermino = pj.DataTermino,
                             StatusIcon = sta.Icon,
                             Image = paramRep.GetImage(pj.ProjetoId, "images", "Projetos", "Projeto", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()) || a.Docente.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<ProjetoViewModel> Report()
        {
            IUserRepository _userRep = new UserRepository();
            IStatusRepository _staRep = new StatusRepository();

            return (from pj in this.GetAll()
                         join use in _userRep.GetAll() on pj.UserId equals use.UserId
                         join sta in _staRep.GetAll() on pj.StatusId equals sta.StatusId
                         select new ProjetoViewModel
                         {
                             ProjetoId = pj.ProjetoId,
                             UserId = pj.UserId,
                             StatusId = pj.StatusId,
                             Docente = use.Nome,
                             Nome = pj.Nome,
                             Status = sta.Descricao,
                             CargaHoraria = pj.CargaHoraria,
                             DataCadastro = pj.DataCadastro,
                             DataInicio = pj.DataInicio,
                             DataTermino = pj.DataTermino
                         });
        }

        public IEnumerable<DashProjetosViewModel> Dash()
        {
            IUserRepository _userRep = new UserRepository();
            IStatusRepository _staRep = new StatusRepository();

            return (from pj in this.GetAll()
                         join use in _userRep.GetAll() on pj.UserId equals use.UserId
                         join sta in _staRep.GetAll() on pj.StatusId equals sta.StatusId
                         select new DashProjetosViewModel
                         {
                             ProjetoId = pj.ProjetoId,
                             StatusId = pj.StatusId,
                             Descricao = pj.Nome,
                             DocenteId = pj.UserId,
                             Docente = use.Nome,
                             CargaHoraria = pj.CargaHoraria,
                             DataInicio = pj.DataInicio,
                             DataTermino = pj.DataTermino,
                             Progresso = 10,
                             Status = sta.Descricao,
                             Classe = sta.Classe,
                             Cor = sta.Cor
                         });
        }
    }
}