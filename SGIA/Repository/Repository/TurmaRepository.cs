using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class TurmaRepository : RepositoryBase<Turma>, ITurmaRepository
    {
        public override Turma Add(Turma Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Turma> AddAll(List<Turma> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<TurmaViewModel> Grid(string Buscar, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null)
        {
            IUserRepository useRep = new UserRepository();

            var Turmas = this.GetAll();

            var Model = (from tur in Turmas
                         join use in useRep.GetAll() on tur.CoordenadorId equals use.UserId
                         select new TurmaViewModel
                         {
                             TormaId = tur.TurmaId,
                             CoordenadorId = tur.CoordenadorId,
                             Coordenador = use,
                             Descricao = tur.Descricao,
                             Duracao = tur.Duracao,
                             QtdeSemestres = tur.QtdeSemestres,
                             Name = tur.Nome,
                         });


            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Coordenador.Nome.ToLower().Contains(Buscar.ToLower()));

            //if (StatusId != null)
            //    Model = Model.Where(a => a.StatusId == StatusId);

            //if (DataInicial != null)
            //    Model = Model.Where(a => a.DataCadastro >= DataInicial);

            //if (DataFinal != null)
            //    Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<TurmaViewModel> Report()
        {
            IUserRepository useRep = new UserRepository();

            var Turmas = this.GetAll();

            return (from tur in Turmas
                         join use in useRep.GetAll() on tur.CoordenadorId equals use.UserId
                         select new TurmaViewModel
                         {
                             TormaId = tur.TurmaId,
                             CoordenadorId = tur.CoordenadorId,
                             Coordenador = use,
                             Descricao = tur.Descricao,
                             Duracao = tur.Duracao,
                             QtdeSemestres = tur.QtdeSemestres,
                             Name = tur.Nome,
                         });
        }
    }
}