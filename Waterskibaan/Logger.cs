using System.Collections.Generic;

namespace Waterskibaan
{
    public class Logger
    {
        private List<Sporter> Visitors { get; set; }

        public Logger()
        {
            Visitors = new List<Sporter>();
        }
        
        public void AddVisitor(Sporter visitor)
        {
            Visitors.Add(visitor);
        }
    }
}