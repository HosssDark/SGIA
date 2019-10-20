using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public override User Add(User Entity)
        {
            Entity.DataCadastro = DateTime.Now;
            Entity.StatusId = 1;
            Entity.TipoAcessoId = 2;

            return base.Add(Entity);
        }

        public override List<User> AddAll(List<User> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
                item.StatusId = 1;
                item.TipoAcessoId = 2;
            }

            return base.AddAll(List);
        }

        public User VerificationEmail(string Email)
        {
            return this.Get(a => a.Email == Email).FirstOrDefault();
        }
    }
}