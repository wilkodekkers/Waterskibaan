using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    class Omdraaien : IMoves
    {
        public int Move()
        {
            return new Random().Next(100) > 50 ? 3 : 0;
        }
    }
}
