using System.Collections.Generic;
using System.Linq;

namespace Waterskibaan
{
    public abstract class Wachtrij
    {
        public virtual int MaxLengteRij { get => 5; }
        private Queue<Sporter> Queue { get; set; } = new Queue<Sporter>();

        public abstract bool CheckAantal(int aantal);

        public void SporterNeemPlaatsInRij(Sporter sporter)
        {
            if (CheckAantal(Queue.Count + 1))
            {
                Queue.Enqueue(sporter);
            }
        }

        public List<Sporter> GetAlleSporters()
        {
            return Queue.ToList();
        }

        public List<Sporter> SportersVerlatenRij(int aantal)
        {
            int amount = aantal > Queue.Count ? Queue.Count : aantal;
            List<Sporter> sporters = new List<Sporter>();

            for (int i = 0; i < amount; i++)
            {
                sporters.Add(Queue.Dequeue());
            }

            return sporters;
        }

        public override string ToString()
        {
            return $"Wachtrij: {Queue.Count} sporters";
        }
    }
}
