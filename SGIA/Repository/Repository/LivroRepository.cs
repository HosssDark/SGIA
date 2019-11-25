using Domain;
using Repository.Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class LivroRepository : RepositoryBase<Livro>, ILivroRepository
    {
        public override Livro Add(Livro Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Livro> AddAll(List<Livro> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
            }

            return base.AddAll(List);
        }

        public IEnumerable<LivroViewModel> Grid(string Buscar, int? StatusId = null, int? EditoraId = null, DateTime? DataInicial = null, DateTime? DataFinal = null, string Direct = "")
        {
            IEditoraRepository _ediRep = new EditoraRepository();
            IStatusRepository _staRep = new StatusRepository();
            IParamDirectoryRepository paramRep = new ParamDirectoryRepository();

            var Model = (from lv in this.GetAll()
                         join ed in _ediRep.GetAll() on lv.EditoraId equals ed.EditoraId
                         join sta in _staRep.GetAll() on lv.StatusId equals sta.StatusId
                         select new LivroViewModel
                         {
                             LivroId = lv.LivroId,
                             AreaConhecimento = lv.AreaConhecimento,
                             Autor = lv.Autor,
                             DataCadastro = lv.DataCadastro,
                             DataPublicacao = lv.DataPublicacao,
                             EditoraId = lv.EditoraId,
                             Editora = ed.Nome,
                             StatusId = lv.StatusId,
                             Status = sta.Descricao,
                             StatusIcon = sta.Icon,
                             Titulo = lv.Titulo,
                             Image = paramRep.GetImage(lv.LivroId, "images", "Livros", "Livro", Direct)
                         });

            #region + Filtro

            if (!string.IsNullOrEmpty(Buscar))
                Model = Model.Where(a => a.Titulo.ToLower().Contains(Buscar.ToLower()) || a.Autor.ToLower().Contains(Buscar.ToLower()));

            if (StatusId != null)
                Model = Model.Where(a => a.StatusId == StatusId);

            if (EditoraId != null)
                Model = Model.Where(a => a.EditoraId == EditoraId);

            if (DataInicial != null)
                Model = Model.Where(a => a.DataCadastro >= DataInicial);

            if (DataFinal != null)
                Model = Model.Where(a => a.DataCadastro <= DataFinal);

            #endregion

            return Model;
        }

        public IEnumerable<LivroViewModel> Report()
        {
            IEditoraRepository _ediRep = new EditoraRepository();
            IStatusRepository _staRep = new StatusRepository();

            return (from lv in this.GetAll()
                    join ed in _ediRep.GetAll() on lv.EditoraId equals ed.EditoraId
                    join sta in _staRep.GetAll() on lv.StatusId equals sta.StatusId
                    select new LivroViewModel
                    {
                        LivroId = lv.LivroId,
                        AreaConhecimento = lv.AreaConhecimento,
                        Autor = lv.Autor,
                        DataCadastro = lv.DataCadastro,
                        DataPublicacao = lv.DataPublicacao,
                        EditoraId = lv.EditoraId,
                        Editora = ed.Nome,
                        StatusId = lv.StatusId,
                        Status = sta.Descricao,
                        Titulo = lv.Titulo
                    });
        }
    }
}