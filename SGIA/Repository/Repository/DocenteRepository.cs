using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class DocenteRepository : RepositoryBase<Docente>, IDocenteRepository
    {
        public override Docente Add(Docente Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<Docente> AddAll(List<Docente> List)
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