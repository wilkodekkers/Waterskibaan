namespace Waterskibaan
{
    public class Lijn
    {
        public int PositieOpDeKabel { get; set; }
        public Sporter Sporter { get; set; }

        public Lijn()
        {
            PositieOpDeKabel = 0;
        }
    }
}
