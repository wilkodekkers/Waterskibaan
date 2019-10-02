namespace Waterskibaan
{
    class InstructieGroep : Wachtrij
    {
        public override int MaxLengteRij => 5;

        public void OnInstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            foreach (Sporter sporter in args.SportersNieuw)
            {
                SporterNeemPlaatsInRij(sporter);
            }
        }

        public override string ToString()
        {
            return $"Instructie groep: {GetAlleSporters().Count} sporters";
        }
    }
}
