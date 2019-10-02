namespace Waterskibaan
{
    class WachtrijStarten : Wachtrij
    {
        public override int MaxLengteRij => 20;

        public void OnInstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            foreach (Sporter sporter in args.SportersKlaar)
            {
                SporterNeemPlaatsInRij(sporter);
            }
        }

        public override string ToString()
        {
            return $"Wachtrij starten: {GetAlleSporters().Count} sporters";
        }
    }
}
