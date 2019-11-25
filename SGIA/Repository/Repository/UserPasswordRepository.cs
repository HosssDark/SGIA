using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public UserPassword VerificationPassword(string Email, string Password)
        {
            return this.GetFirst(a => a.Password == this.MD5Hash(Password + Email));
        }

        public string UserRegister(string Email, string Password)
        {
            IUserRepository RepositoryUser = new UserRepository();

            User User = new User()
            {
                Email = Email
            };

            RepositoryUser.Add(User);

            UserPassword UserPassword = new UserPassword()
            {
                Password = this.MD5Hash(Password + User.Email),
                Guid = Guid.NewGuid().ToString(),
                UserId = User.UserId
            };

            this.Add(UserPassword);

            return UserPassword.Guid;
        }

        public void ChangePassword(int UserId, string Password)
        {
            IUserRepository RepositoryUser = new UserRepository();

            var Model = this.Get(a => a.UserId == UserId).FirstOrDefault();
            var User = RepositoryUser.GetById(UserId);

            if (Model != null)
            {
                Model.Password = this.MD5Hash(Password + User.Email);

                this.Attach(Model);
            }
        }

        public void ChangePassword(string Guid, string Email, string Password)
        {
            IUserRepository RepositoryUser = new UserRepository();

            var Model = this.Get(a => a.Guid == Guid).FirstOrDefault();

            if(Model != null)
            {
                Model.Password = this.MD5Hash(Password + Email);
                Model.Guid = null;

                this.Attach(Model);
            }
        }

        public string ChangeGuid(string Email)
        {
            IUserRepository user = new UserRepository();

            var User = user.Get(a => a.Email == Email).FirstOrDefault();
            var Model = this.Get(a => a.UserId == User.UserId).FirstOrDefault();

            if (Model != null)
            {
                Model.Guid = Guid.NewGuid().ToString();

                this.Attach(Model);
            }

            return Model.Guid;
        }

        public string MD5Hash(string Password)
        {
            string Hash = "!@#$%¨&*123456789?" + Password;

            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(Hash);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}