using Domain;
using System;
using System.Collections.Generic;

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
    }
}