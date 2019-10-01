namespace Waterskibaan
{
    public class Lijn
    {
        private int _positieOpDeKabel;
        private Sporter _sporter;

        public int PositieOpDeKabel { get => _positieOpDeKabel; set => _positieOpDeKabel = value; }
        public Sporter Sporter { get => _sporter; set => _sporter = value; }
    }
}
