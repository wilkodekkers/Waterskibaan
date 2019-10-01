using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Waterskibaan;

namespace Tests
{
    [TestClass]
    public class WaterskibaanTests
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestSporterStart()
        {
            Waterskibaan.Waterskibaan waterskibaan = new Waterskibaan.Waterskibaan();

            waterskibaan.SporterStart(new Sporter(MoveCollection.GetWillekeurigeMoves()));
        }
    }
}
