using System;
using System.Collections.Generic;

namespace Waterskibaan
{
    public class Sporter
    {
        public int AantalRondenNogTeGaan { get; set; }
        public Zwemvest Zwemvest { get; set; }
        public Tuple<byte, byte, byte> KledingKleur { get; set; }
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
            KledingKleur = new Tuple<byte, byte, byte>(Convert.ToByte(random.Next(0, 256)), Convert.ToByte(random.Next(0, 256)), Convert.ToByte(random.Next(0, 256)));
        }
    }
}
