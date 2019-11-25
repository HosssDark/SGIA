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

        public IEnumerable<TurmaViewModel> Grid(string Buscar = null, int? StatusId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IUserRepository useRep = new UserRepository();
            IStatusRepository staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Turmas = this.GetAll();

            var Model = (from tur in Turmas
                         join use in useRep.GetAll() on tur.CoordenadorId equals use.UserId into r1
                         from use in r1.DefaultIfEmpty()
                         join sta in staRep.GetAll() on tur.StatusId equals sta.StatusId
                         select new TurmaViewModel
                         {
                             TurmaId = tur.TurmaId,
                             CoordenadorId = tur.CoordenadorId,
                             Coordenador = use != null ? use : null,
                             Descricao = tur.Descricao,
                             Duracao = tur.Duracao,
                             QtdeSemestres = tur.QtdeSemestres,
                             DataCadastro = tur.DataCadastro,
                             Name = tur.Nome,
                             QtdeEstudantes = this.GetDicenteCount(tur.TurmaId),
                             StatusId = tur.StatusId,
                             Status = sta.Descricao,
                             StatusIcon = sta.Icon,
                             Image = paramRep.GetImage(tur.TurmaId, "images", "Turmas", "Turma", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Coordenador.Nome.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

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
                        TurmaId = tur.TurmaId,
                        CoordenadorId = tur.CoordenadorId,
                        Coordenador = use,
                        Descricao = tur.Descricao,
                        Duracao = tur.Duracao,
                        QtdeSemestres = tur.QtdeSemestres,
                        Name = tur.Nome,
                    });
        }

        public int GetDicenteCount(int TurmaId)
        {
            IDicenteRepository dic = new DicenteRepository();

            return dic.Get(a => a.TurmaId == TurmaId).Count();
        }
    }
}