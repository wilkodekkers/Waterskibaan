using System;

namespace Waterskibaan
{
    public class Waterskibaan
    {
        private Kabel Kabel { get; set; }
        private LijnenVoorraad LijnenVoorraad { get; set; }

        public Waterskibaan()
        {
            Kabel = new Kabel();
            LijnenVoorraad = new LijnenVoorraad();

            for (int i = 0; i < 15; i++)
                LijnenVoorraad.LijnToevoegenAanRij(new Lijn());
        }

        public void VerplaatsKabel()
        {
            Kabel.VerschuifLijnen();

            Lijn lijn = Kabel.VerwijderLijnVanKabel();

            if (lijn != null)
                LijnenVoorraad.LijnToevoegenAanRij(lijn);
        }

        public void SporterStart(Sporter sp)
        {
            if (sp.Skies == null || sp.Zwemvest == null)
                throw new Exception("Een sporter heeft skies en een zwemvest nodig!");

            if (!Kabel.IsStartPositieLeeg()) return;

            Lijn lijn = LijnenVoorraad.VerwijderEersteLijn();

            lijn.Sporter = sp;
            lijn.Sporter.AantalRondenNogTeGaan = new Random().Next(1, 3);

            Kabel.NeemLijnInGebruik(lijn);
        }

        public override string ToString()
        {
            return $"{LijnenVoorraad} | {Kabel}";
        }
    }
}
