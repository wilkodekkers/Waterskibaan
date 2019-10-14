using System;
using System.Collections.Generic;

namespace Waterskibaan
{
    public class Sporter
    {
        public int AantalRondenNogTeGaan { get; set; }
        public Zwemvest Zwemvest { get; set; }
        public string KledingKleur { get; set; }
        public Skies Skies { get; set; }
        public List<IMoves> Moves { get; set; }
        public int Score { get; set; }
        public IMoves HuidigeMove { get; set; }

        public Sporter(List<IMoves> moves)
        {
            Zwemvest = null;
            Moves = moves;
            Score = 0;
            HuidigeMove = null;

            Random random = new Random();
            KledingKleur = string.Format("#{0:X6}", random.Next(0x1000000));
        }
    }
}
