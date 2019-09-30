using System.Collections.Generic;

namespace Waterskibaan
{
    public class LijnenVoorraad
    {
        private Queue<Lijn> _lijnen = new Queue<Lijn>();

        public void LijnToevoegenAanRij(Lijn lijn)
        {
            _lijnen.Enqueue(lijn);
        }

        public Lijn VerwijderEersteLijn()
        {
            if (_lijnen.Count == 0) return null;

            return _lijnen.Dequeue();
        }

        public int GetAantalLijnen()
        {
            return _lijnen.Count;
        }

        public override string ToString()
        {
            return $"{GetAantalLijnen()} lijnen op voorraad";
        }
    }
}
