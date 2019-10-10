using Microsoft.VisualStudio.TestTools.UnitTesting;
using Waterskibaan;
using System;
using System.Collections.Generic;

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

            kabel.NeemLijnInGebruik(lijn);
            lijn.PositieOpDeKabel = 1;
            //kabel.VerschuifLijnen();

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

            kabel.NeemLijnInGebruik(lijn);

            Assert.AreEqual(kabel.Lijnen.Count, 1);
        }
    }
}
