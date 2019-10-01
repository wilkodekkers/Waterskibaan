using System.Collections.Generic;
using System.Drawing;

namespace Waterskibaan
{
    class Sporter
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
        }
    }
}
