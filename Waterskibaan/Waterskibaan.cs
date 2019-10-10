using System;

namespace Waterskibaan
{
    public class Waterskibaan
    {
        public Kabel Kabel { get; set; } = new Kabel();
        private LijnenVoorraad LijnenVoorraad { get; set; } = new LijnenVoorraad();

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

            Lijn lijn = LijnenVoorraad.VerwijderEersteLijn();
            lijn.Sporter = sp;
            sp.AantalRondenNogTeGaan = new Random().Next(1, 3);
            Kabel.NeemLijnInGebruik(lijn);
        }

        public override string ToString()
        {
            return $"{LijnenVoorraad} | {Kabel}";
        }
    }
}
