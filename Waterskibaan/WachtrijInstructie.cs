namespace Waterskibaan
{
    class WachtrijInstructie : Wachtrij
    {
        public override int MaxLengteRij => 100;

        public WachtrijInstructie(Game game)
        {
            game.NieuweBezoeker += OnNieuweBezoeker;
        }

        private void OnNieuweBezoeker(NieuweBezoekerArgs args)
        {
            SporterNeemPlaatsInRij(args.Sporter);
        }

        public override string ToString()
        {
            return $"Wachtrij instrucie: {GetAlleSporters().Count} sporters";
        }
    }
}
