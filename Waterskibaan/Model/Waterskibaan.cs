using System;

namespace Waterskibaan
{
    public class Waterskibaan
    {
        public Kabel Kabel { get; set; } = new Kabel();
        public LijnenVoorraad LijnenVoorraad { get; set; } = new LijnenVoorraad();

        public Waterskibaan()
        {
            for (int i = 0; i < 15; i++)
                LijnenVoorraad.LijnToevoegenAanRij(new Lijn());
        }

        public void VerplaatsKabel()
        {
            Kabel.VerschuifLijnen();

            Lijn lijn = Kabel.VerwijderLijnVanKabel();

            if (lijn != null)
            {
                LijnenVoorraad.LijnToevoegenAanRij(lijn);
            }
        }

        public void SporterStart(Sporter sp)
        {
            if (!Kabel.IsStartPositieLeeg()) return;

            if (sp.Skies == null || sp.Zwemvest == null)
            {
                throw new Exception("Een sporter heeft skies en een zwemvest nodig!");
            }

            var line = LijnenVoorraad.VerwijderEersteLijn();
            line.Sporter = sp;
            sp.AantalRondenNogTeGaan = new Random().Next(1, 3);
            Kabel.NeemLijnInGebruik(line);
        }

        public override string ToString()
        {
            return $"{LijnenVoorraad} | {Kabel}";
        }
    }
}
