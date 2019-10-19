using System;
using System.Collections.Generic;
using System.Text;

namespace Librarie
{
    public class LoginUser
    {
        private string Key = "Login.User";
        private Session.Session _Session;

        public LoginUser(Session.Session session)
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
