using System;
using System.Collections.Generic;
using Domain;

namespace Repository
{
    public class DiciplinaRepository : RepositoryBase<Diciplina>, IDiciplinaRepository
    {
        public override Diciplina Add(Diciplina Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Diciplina> AddAll(List<Diciplina> List)
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