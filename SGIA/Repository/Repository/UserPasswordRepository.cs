using Domain;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class UserPasswordRepository : RepositoryBase<UserPassword>, IUserPasswordRepository
    {
        public override UserPassword Add(UserPassword Entity)
        {
            Entity.DataCadastro = DateTime.Now;

            return base.Add(Entity);
        }

        public override List<UserPassword> AddAll(List<UserPassword> List)
        {
            foreach (var item in List)
            {
                item.DataCadastro = DateTime.Now;
            }

            return base.AddAll(List);
        }

        public UserPassword VerificationPassword(string Password)
        {
            return this.GetFirst(a => a.Password == Password);
        }
    }
}