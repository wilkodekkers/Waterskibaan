using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Waterskibaan
{
    public class Sporter
    {
        private int _aantalRondenNogTeGaan;
        private Zwemvest _zwemvest;
        private Skies _skies;
        private Color _kledingKleur;
        private List<IMoves> _moves;
        private int _score;

        public int AantalRondenNogTeGaan { get => _aantalRondenNogTeGaan; set => _aantalRondenNogTeGaan = value; }
        public Zwemvest Zwemvest { get => _zwemvest; set => _zwemvest = value; }
        public Color KledingKleur { get => _kledingKleur; set => _kledingKleur = value; }
        public Skies Skies { get => _skies; set => _skies = value; }
        public List<IMoves> Moves { get => _moves; set => _moves = value; }
        public int Score { get => _score; set => _score = value; }

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
