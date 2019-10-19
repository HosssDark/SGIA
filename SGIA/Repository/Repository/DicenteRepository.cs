using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class DicenteRepository : RepositoryBase<Dicente>, IDicenteRepository
    {
        public override Dicente Add(Dicente Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Dicente> AddAll(List<Dicente> List)
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