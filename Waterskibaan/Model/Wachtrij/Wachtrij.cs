using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waterskibaan
{
    public abstract class Wachtrij
    {
        public virtual int MaxLengteRij { get => 5; }

        private Queue<Sporter> _queue = new Queue<Sporter>();

        public abstract bool CheckAantal(int aantal);

        public void SporterNeemPlaatsInRij(Sporter sporter)
        {
            if (CheckAantal(_queue.Count + 1))
            {
                _queue.Enqueue(sporter);
            }
            else
            {
                throw new Exception("De wachtrij is vol!");
            }
        }

        public List<Sporter> GetAlleSporters()
        {
            return _queue.ToList();
        }

        public List<Sporter> SportersVerlatenRij(int aantal)
        {
            int amount = aantal > _queue.Count ? _queue.Count : aantal;
            List<Sporter> sporters = new List<Sporter>();

            for (int i = 0; i < amount; i++)
            {
                sporters.Add(_queue.Dequeue());
            }

            return sporters;
        }

        public override string ToString()
        {
            return $"Wachtrij: {_queue.Count} sporters";
        }
    }
}
