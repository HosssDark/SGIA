using Domain;
using Newtonsoft.Json;

namespace Site
{
    public class LoginUser
    {
        private string Key = "Login.User";
        private Session _Session;

        public LoginUser(Session session)
        {
            _Session = session;
        }

        public void SetUser(User User)
        {
            string Value = JsonConvert.SerializeObject(User);

            _Session.Register(Key, Value);
        }

        public User GetUser()
        {
            return JsonConvert.DeserializeObject<User>(_Session.Get(Key));
        }

        public void Exit()
        {
            _Session.RemoveAll();
        }
    }
}