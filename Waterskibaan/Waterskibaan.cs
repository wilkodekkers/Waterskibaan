using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    public class Waterskibaan
    {
        private Kabel _kabel = new Kabel();
        private LijnenVoorraad _lijnen = new LijnenVoorraad();

        public Waterskibaan()
        {
            for (int i = 0; i < 15; i++)
            {
                _lijnen.LijnToevoegenAanRij(new Lijn());
            }
        }

        public void VerplaatsKabel()
        {
            _kabel.VerschuifLijnen();

            Lijn lijn = _kabel.VerwijderLijnVanKabel();

            if (lijn != null)
            {
                _lijnen.LijnToevoegenAanRij(lijn);
            }
        }

        public void SporterStart(Sporter sp)
        {
            if (sp.Skies == null || sp.Zwemvest == null) throw new Exception("Een sporter heeft skies en een zwemvest nodig!");

            if (!_kabel.IsStartPositieLeeg()) return;

            Lijn lijn = _lijnen.VerwijderEersteLijn();

            lijn.Sporter = sp;
            lijn.Sporter.AantalRondenNogTeGaan = new Random().Next(1, 3);

            _kabel.NeemLijnInGebruik(lijn);
        }

        public override string ToString()
        {
            return $"{_lijnen} | {_kabel}";
        }
    }
}
