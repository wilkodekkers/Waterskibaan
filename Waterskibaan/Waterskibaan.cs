using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    class Waterskibaan
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

        public override string ToString()
        {
            return $"{_lijnen} | {_kabel}";
        }
    }
}
