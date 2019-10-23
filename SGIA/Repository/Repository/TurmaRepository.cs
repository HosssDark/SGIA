using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class TurmaRepository : RepositoryBase<Turma>, ITurmaRepository
    {
        public override Turma Add(Turma Entity)
        {
            Entity.DataCadastro = DateTime.Now;

            return base.Add(Entity);
        }

        public override List<Turma> AddAll(List<Turma> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
            }

            return base.AddAll(List);
        }
    }
}