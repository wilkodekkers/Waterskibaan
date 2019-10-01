using System.Collections.Generic;
using System.Linq;

namespace Waterskibaan
{
    public class Kabel
    {
        private LinkedList<Lijn> _lijnen = new LinkedList<Lijn>();

        public bool IsStartPositieLeeg()
        {
            if (_lijnen.Count == 0 || _lijnen.First().PositieOpDeKabel != 0) return true;

            return false;
        }

        public void NeemLijnInGebruik(Lijn lijn)
        {
            if (IsStartPositieLeeg())
            {
                _lijnen.AddFirst(lijn);
            }
        }

        public void VerschuifLijnen()
        {
            foreach (Lijn lijn in _lijnen)
            {
                if (lijn.PositieOpDeKabel == 9)
                {
                    lijn.Sporter.AantalRondenNogTeGaan--;
                    lijn.PositieOpDeKabel = 0;
                }
                else
                {
                    lijn.PositieOpDeKabel++;
                }
            }
        }

        public Lijn VerwijderLijnVanKabel()
        {
            Lijn found = null;

            foreach (Lijn lijn in _lijnen)
            {
                if (lijn.PositieOpDeKabel == 9 && lijn.Sporter.AantalRondenNogTeGaan == 1)
                {
                    found = lijn;
                    break;
                }
            }

            return found;
        }

        public override string ToString()
        {
            if (_lijnen.Count == 0) return "";

            string result = "";

            foreach (Lijn lijn in _lijnen)
            {
                result += $"{lijn.PositieOpDeKabel}|";
            }

            return result.Substring(0, result.Length - 1);
        }
    }
}
