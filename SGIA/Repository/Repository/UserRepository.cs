using Domain;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public User VerificationEmail(string Email)
        {
            return this.GetFirst(a => a.Email == Email);
        }
    }
}