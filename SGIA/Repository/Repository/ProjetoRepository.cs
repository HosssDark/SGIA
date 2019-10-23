using Domain;
using System;
using System.Collections.Generic;

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
    }
}