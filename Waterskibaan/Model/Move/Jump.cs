using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    class Jump : IMoves
    {
        public int Move()
        {
            return new Random().Next(100) > 30 ? 2 : 0;
        }

        public override string ToString()
        {
            return "Jump";
        }
    }
}
