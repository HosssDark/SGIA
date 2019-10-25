using System.Collections.Generic;

namespace Site
{
    public class Notification
    {
        public class NotificationList
        {
            public string Type { get; set; }
            public IEnumerable<string> Message { get; set; }
        }
    }
}