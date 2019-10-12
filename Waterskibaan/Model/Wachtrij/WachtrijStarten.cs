namespace Waterskibaan
{
    class WachtrijStarten : Wachtrij
    {
        public override int MaxLengteRij => 20;

        public override bool CheckAantal(int aantal)
        {
            return aantal <= MaxLengteRij;
        }

        public void OnInstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            foreach (Sporter sporter in args.SportersKlaar)
            {
                SporterNeemPlaatsInRij(sporter);
            }
        }

        public override string ToString()
        {
            return $"Wachtrij starten: {GetAlleSporters().Count} wachtenden";
        }
    }
}
