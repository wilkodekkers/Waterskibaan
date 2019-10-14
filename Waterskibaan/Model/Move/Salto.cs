using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    class Salto : IMoves
    {
        public int Move()
        {
            return new Random().Next(100) > 70 ? 6 : 0;
        }
        
        public override string ToString()
        {
            return "Salto";
        }
    }
}
