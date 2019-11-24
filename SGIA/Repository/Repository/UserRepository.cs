using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public override User Add(User Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;
            Entity.TipoAcessoId = 2;

            return base.Add(Entity);
        }

        public override List<User> AddAll(List<User> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
                item.TipoAcessoId = 2;
            }

            return base.AddAll(List);
        }

        public User VerificationEmail(string Email)
        {
            return this.Get(a => a.Email == Email).FirstOrDefault();
        }

        public IEnumerable<UserViewModel> Grid(string Buscar, int? StatusId = null, int? AreaAtuacaoId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IAreaAtuacaoRepository _areRep = new AreaAtuacaoRepository();
            ITituloRepository _titRep = new TituloRepository();
            ITipoDocenteRepository _tipRep = new TipoDocenteRepository();
            ITipoAcessoRepository _aceRep = new TipoAcessoRepository();
            IStatusRepository _staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Model = (from use in this.GetAll()
                         join at in _areRep.GetAll() on use.AreaAtuacaoId equals at.AreaAtuacaoId into r1
                         from at in r1.DefaultIfEmpty()
                         join tl in _titRep.GetAll() on use.TituloId equals tl.TituloId into r2
                         from tl in r2.DefaultIfEmpty()
                         join tp in _tipRep.GetAll() on use.TipoId equals tp.TipoDocenteId into r3
                         from tp in r3.DefaultIfEmpty()
                         join ace in _aceRep.GetAll() on use.TipoAcessoId equals ace.TipoAcessoId into r4
                         from ace in r4.DefaultIfEmpty()
                         join sta in _staRep.GetAll() on use.StatusId equals sta.StatusId
                         select new UserViewModel
                         {
                             UserId = use.UserId,
                             TituloId = use.TituloId,
                             TipoId = use.TipoId,
                             TipoAcessoId = use.TipoAcessoId,
                             StatusId = use.StatusId,
                             CargaHoraria = use.CargaHoraria,
                             Celular = use.Celular,
                             Email = use.Email,
                             EmailLattes = use.EmailLattes,
                             AreaAtuacaoId = use.AreaAtuacaoId,
                             AreaAtuacao = at != null ? at.Descricao : "",
                             Tipo = tp != null ? tp.Descricao : "",
                             TipoAcesso = ace != null ? ace.Descricao : "",
                             Titulo = tl != null ? tl.Descricao : "",
                             Status = sta.Descricao,
                             Image = paramRep.GetImageUser(use.UserId, "images", "Usuarios", "Usuario", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Nome.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (AreaAtuacaoId != null)
                Model = Model.Where(a => a.AreaAtuacaoId == AreaAtuacaoId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<UserViewModel> Report()
        {
            IAreaAtuacaoRepository _areRep = new AreaAtuacaoRepository();
            ITituloRepository _titRep = new TituloRepository();
            ITipoDocenteRepository _tipRep = new TipoDocenteRepository();
            ITipoAcessoRepository _aceRep = new TipoAcessoRepository();
            IStatusRepository _staRep = new StatusRepository();

            return (from use in this.GetAll()
                         join at in _areRep.GetAll() on use.AreaAtuacaoId equals at.AreaAtuacaoId into r1
                         from at in r1.DefaultIfEmpty()
                         join tl in _titRep.GetAll() on use.TituloId equals tl.TituloId into r2
                         from tl in r2.DefaultIfEmpty()
                         join tp in _tipRep.GetAll() on use.TipoId equals tp.TipoDocenteId into r3
                         from tp in r3.DefaultIfEmpty()
                         join ace in _aceRep.GetAll() on use.TipoAcessoId equals ace.TipoAcessoId into r4
                         from ace in r4.DefaultIfEmpty()
                         join sta in _staRep.GetAll() on use.StatusId equals sta.StatusId
                         select new UserViewModel
                         {
                             UserId = use.UserId,
                             TituloId = use.TituloId,
                             TipoId = use.TipoId,
                             TipoAcessoId = use.TipoAcessoId,
                             StatusId = use.StatusId,
                             CargaHoraria = use.CargaHoraria,
                             Celular = use.Celular,
                             Email = use.Email,
                             EmailLattes = use.EmailLattes,
                             AreaAtuacaoId = use.AreaAtuacaoId,
                             AreaAtuacao = at != null ? at.Descricao : "",
                             Tipo = tp != null ? tp.Descricao : "",
                             TipoAcesso = ace != null ? ace.Descricao : "",
                             Titulo = tl != null ? tl.Descricao : "",
                             Status = sta.Descricao,
                         });
        }
    }
}