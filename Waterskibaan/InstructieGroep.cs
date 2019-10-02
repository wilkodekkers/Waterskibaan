namespace Waterskibaan
{
    class InstructieGroep : Wachtrij
    {
        public override int MaxLengteRij => 5;

        public InstructieGroep(Game game)
        {
            game.InstructieAfgelopen += OnInstructieAfgelopen;
        }

        private void OnInstructieAfgelopen(InstructieAfgelopenArgs args)
        {
            foreach (Sporter sporter in args.Sporters)
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
