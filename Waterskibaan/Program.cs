using System;
using System.Collections.Generic;

namespace Waterskibaan
{
    class Program
    {
        static void Main(string[] args)
        {
            Kabel kabel = new Kabel();
            Lijn lijn = new Lijn();
            List<IMoves> moves = MoveCollection.GetWillekeurigeMoves();
            Sporter sporter = new Sporter(moves);

            lijn.Sporter = sporter;
            lijn.PositieOpDeKabel = 9;
            lijn.Sporter.AantalRondenNogTeGaan = 1;

            kabel.NeemLijnInGebruik(lijn);
            Lijn lijn2 = kabel.VerwijderLijnVanKabel();

            Console.WriteLine(lijn == lijn2);
        }
    }
}
