using System;
using System.Collections.Generic;

namespace Waterskibaan
{
    public class LijnenVerplaatsArgs : EventArgs
    {
        public Sporter Sporter { get; set; }
        public LinkedList<Lijn> Lijnen { get; set; }
    }
}
