using System;

namespace Domain
{
    public class Log
    {
        public int LogId { get; set; }
        public int UserChangeId { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public DateTime DateChange { get; set; }
    }
}