using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    class OpEenBeen : IMoves
    {
        public int Move()
        {
            return new Random().Next(100) > 60 ? 4 : 0;
        }
    }
}
