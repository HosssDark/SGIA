using System;
using System.Collections.Generic;
using Domain;

namespace Repository
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        public override Log Add(Log Entity)
        {
            Entity.DateChange = DateTime.Now;

            return base.Add(Entity);
        }

        public override List<Log> AddAll(List<Log> List)
        {
            foreach (var item in List)
            {
                item.DateChange = DateTime.Now;
            }

            return base.AddAll(List);
        }
    }
}