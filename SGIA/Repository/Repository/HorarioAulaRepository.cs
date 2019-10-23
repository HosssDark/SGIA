using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class HorarioAulaRepository : RepositoryBase<HorarioAula>, IHorarioAulaRepository
    {
        public override HorarioAula Add(HorarioAula Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<HorarioAula> AddAll(List<HorarioAula> List)
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