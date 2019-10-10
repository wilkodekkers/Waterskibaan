using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Waterskibaan
{
    public class Sporter
    {
        public int AantalRondenNogTeGaan { get; set; }
        public Zwemvest Zwemvest { get; set; }
        public Color KledingKleur { get; set; }
        public Skies Skies { get; set; }
        public List<IMoves> Moves { get; set; }
        public int Score { get; set; }

        public Sporter(List<IMoves> moves)
        {
            Moves = moves;

            Random random = new Random();
            byte[] b = new byte[3];
            random.NextBytes(b);
            KledingKleur = Color.FromRgb(b[0], b[1], b[2]);
        }
    }
}
