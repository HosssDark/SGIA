﻿using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class DiciplinaLivroRepository : RepositoryBase<DiciplinaLivro>, IDiciplinaLivroRepository
    {
        public override DiciplinaLivro Add(DiciplinaLivro Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;

            return base.Add(Entity);
        }

        public override List<DiciplinaLivro> AddAll(List<DiciplinaLivro> List)
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