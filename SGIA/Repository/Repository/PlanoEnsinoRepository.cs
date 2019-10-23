using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class PlanoEnsinoRepository : RepositoryBase<PlanoEnsino>, IPlanoEnsinoRepository
    {
        public override PlanoEnsino Add(PlanoEnsino Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<PlanoEnsino> AddAll(List<PlanoEnsino> List)
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