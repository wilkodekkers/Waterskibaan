using System.Collections.Generic;

namespace Waterskibaan
{
    public class LijnenVoorraad
    {
        private Queue<Lijn> Lijnen { get; set; }

        public LijnenVoorraad()
        {
            Lijnen = new Queue<Lijn>();
        }

        public int GetCount()
        {
            return Lijnen.Count;
        }

        public void LijnToevoegenAanRij(Lijn lijn)
        {
            Lijnen.Enqueue(lijn);
        }

        public Lijn VerwijderEersteLijn()
        {
            Lijn lijn = null;

            if (Lijnen.Count > 0)
                lijn = Lijnen.Dequeue();

            return lijn;
        }

        public int GetAantalLijnen()
        {
            return Lijnen.Count;
        }

        public override string ToString()
        {
            return $"{GetAantalLijnen()} lijnen op voorraad";
        }
    }
}
