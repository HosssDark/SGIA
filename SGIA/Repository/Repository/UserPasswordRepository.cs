using Domain;

namespace Repository
{
    public class UserPasswordRepository : RepositoryBase<UserPassword>, IUserPasswordRepository
    {
        public UserPassword VerificationPassword(string Password)
        {
            return this.GetFirst(a => a.Password == Password);
        }
    }
}