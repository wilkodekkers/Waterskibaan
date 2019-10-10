using System;

namespace Waterskibaan
{
    class Program
    {
        static void Main(string[] args)
        {
            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());

            Console.WriteLine(sporter.KledingKleur);
        }
    }
}
