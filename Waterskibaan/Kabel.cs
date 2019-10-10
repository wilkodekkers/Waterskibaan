using System.Collections.Generic;
using System.Linq;

namespace Waterskibaan
{
    public class Kabel
    {
        public LinkedList<Lijn> _lijnen = new LinkedList<Lijn>();

        public bool IsStartPositieLeeg()
        {
            return _lijnen.FirstOrDefault() == null || _lijnen.First().PositieOpDeKabel != 0;
        }

        public void NeemLijnInGebruik(Lijn lijn)
        {
            if (IsStartPositieLeeg())
            {
                lijn.PositieOpDeKabel = 0;
                _lijnen.AddFirst(lijn);
            }
        }

        public void VerschuifLijnen()
        {
            Lijn moveLine = null;

            foreach (Lijn lijn in _lijnen)
            {
                if (lijn.PositieOpDeKabel == 9)
                {
                    moveLine = lijn;
                }
                else
                {
                    lijn.PositieOpDeKabel++;
                }
            }

            if (moveLine != null)
            {
                moveLine.PositieOpDeKabel = 0;

                if (moveLine.Sporter.AantalRondenNogTeGaan != 1)
                {
                    moveLine.Sporter.AantalRondenNogTeGaan--;
                }

                _lijnen.Remove(moveLine);
                _lijnen.AddFirst(moveLine);
            }
        }

        public Lijn VerwijderLijnVanKabel()
        {
            if (_lijnen.Count == 0 || _lijnen.Last.Value.PositieOpDeKabel != 9 || _lijnen.Last.Value.Sporter.AantalRondenNogTeGaan != 1)
            {
                return null;
            }

            Lijn lijn = _lijnen.Last.Value;

            _lijnen.RemoveLast();

            return lijn;
        }

        public override string ToString()
        {
            if (_lijnen.Count == 0) return "";

            string result = "";

            foreach (Lijn lijn in _lijnen)
            {
                result += $"({lijn.PositieOpDeKabel}, {lijn.Sporter.AantalRondenNogTeGaan})|";
            }

            return result.Substring(0, result.Length - 1);
        }
    }
}
