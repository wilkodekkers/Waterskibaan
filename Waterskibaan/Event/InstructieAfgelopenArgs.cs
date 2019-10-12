using System;
using System.Collections.Generic;

namespace Waterskibaan
{
    public class InstructieAfgelopenArgs : EventArgs
    {
        public List<Sporter> SportersKlaar { get; set; }
        public List<Sporter> SportersNieuw { get; set; }
    }
}
