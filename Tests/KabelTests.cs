using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waterskibaan;

namespace Tests
{
    [TestClass]
    public class KabelTests
    {
        [TestMethod]
        public void TestIsStartPositieLeeg()
        {
            Kabel kabel = new Kabel();

            Assert.AreEqual(kabel.IsStartPositieLeeg(), true);

            kabel.NeemLijnInGebruik(new Lijn());

            Assert.AreEqual(kabel.IsStartPositieLeeg(), false);
        }

        [TestMethod]
        public void TestVerschuifLijnen()
        {
            Kabel kabel = new Kabel();

            kabel.NeemLijnInGebruik(new Lijn());
            kabel.VerschuifLijnen();

            Assert.AreEqual(kabel.IsStartPositieLeeg(), true);
        }

        [TestMethod]
        public void TestVerwijderLijnVanKabel()
        {
            Lijn lijn = new Lijn();
            Kabel kabel = new Kabel();

            kabel.NeemLijnInGebruik(lijn);
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();
            kabel.VerschuifLijnen();

            Lijn removed = kabel.VerwijderLijnVanKabel();

            Assert.AreEqual(lijn, removed);
        }
    }
}
