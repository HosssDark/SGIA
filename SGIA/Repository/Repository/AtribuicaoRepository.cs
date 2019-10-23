using Domain;
using System;
using System.Collections.Generic;

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
    }
}