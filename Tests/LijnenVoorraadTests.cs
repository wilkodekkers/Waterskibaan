using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waterskibaan;

namespace Tests
{
    [TestClass]
    public class LijnenVoorraadTests
    {
        [TestMethod]
        public void TestVerwijderEersteLijn()
        {
            LijnenVoorraad lijnenVoorraad = new LijnenVoorraad();

            Assert.AreEqual(lijnenVoorraad.VerwijderEersteLijn(), null);

            Lijn lijn = new Lijn();

            lijnenVoorraad.LijnToevoegenAanRij(lijn);

            Assert.AreEqual(lijn, lijnenVoorraad.VerwijderEersteLijn());
        }
    }
}
