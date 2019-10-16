using System.Collections.Generic;
using System.Linq;

namespace Waterskibaan
{
    public class Kabel
    {
        public LinkedList<Lijn> Lijnen { get; set; } = new LinkedList<Lijn>();

        public bool IsStartPositieLeeg()
        {
            return Lijnen.FirstOrDefault() == null || Lijnen.All(l => l.PositieOpDeKabel != 0);
        }

        public void NeemLijnInGebruik(Lijn lijn)
        {
            if (IsStartPositieLeeg())
            {
                lijn.PositieOpDeKabel = 0;
                Lijnen.AddFirst(lijn);
            }
        }

        public void VerschuifLijnen()
        {
            Lijn moveLine = null;

            foreach (Lijn lijn in Lijnen)
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

                Lijnen.Remove(moveLine);
                Lijnen.AddFirst(moveLine);
            }
        }

        public Lijn VerwijderLijnVanKabel()
        {
            if (Lijnen.Count == 0 || Lijnen.Last.Value.PositieOpDeKabel != 9 || Lijnen.Last.Value.Sporter.AantalRondenNogTeGaan != 1)
            {
                return null;
            }

            Lijn lijn = Lijnen.Last.Value;

            Lijnen.RemoveLast();

            return lijn;
        }

        public override string ToString()
        {
            if (Lijnen.Count == 0) return "";

            string result = "";

            foreach (Lijn lijn in Lijnen)
            {
                result += $"{lijn.PositieOpDeKabel}|";
            }

            return result.Substring(0, result.Length - 1);
        }
    }
}
