using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waterskibaan;
using System;

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
            Lijn lijn = new Lijn();
            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());
            
            sporter.AantalRondenNogTeGaan = new Random().Next(1, 3);
            lijn.Sporter = sporter;
            kabel.NeemLijnInGebruik(lijn);
            kabel.VerschuifLijnen();

            Assert.AreEqual(kabel.IsStartPositieLeeg(), true);
        }

        [TestMethod]
        public void TestVerwijderLijnVanKabel()
        {
            Kabel kabel = new Kabel();
            Lijn lijn = new Lijn();
            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves())
            {
                Skies = new Skies(),
                Zwemvest = new Zwemvest()
            };

            lijn.Sporter = sporter;
            lijn.Sporter.AantalRondenNogTeGaan = 1;
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

            Lijn testLine = kabel.VerwijderLijnVanKabel();

            Assert.AreEqual(lijn, testLine);
        }
    }
}
