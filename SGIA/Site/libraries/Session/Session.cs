using Microsoft.AspNetCore.Http;

namespace Site
{
    public class Session
    {
        private IHttpContextAccessor _Context;

        public Session(IHttpContextAccessor Context)
        {
            _Context = Context;
        }

        public void Register(string Key, string Value)
        {
            _Context.HttpContext.Session.SetString(Key, Value);
        }

        public void Update(string Key, string Value)
        {
            if (this.Exist(Key))
            {
                _Context.HttpContext.Session.Remove(Key);
                _Context.HttpContext.Session.SetString(Key, Value);
            }
        }

        public string Get(string Key)
        {
           return _Context.HttpContext.Session.GetString(Key);
        }

        public void Remove(string Key)
        {
            _Context.HttpContext.Session.Remove(Key);
        }

        public void RemoveAll()
        {
            _Context.HttpContext.Session.Clear();
        }

        public bool Exist(string Key)
        {
            return _Context.HttpContext.Session.GetString(Key) != null ? true : false;
        }
    }
}