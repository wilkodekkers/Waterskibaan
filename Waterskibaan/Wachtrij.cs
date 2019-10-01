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

        public void SporterNeemPlaatsInRij(Sporter sporter)
        {
            if (_queue.Count >= MaxLengteRij) return;

            _queue.Enqueue(sporter);
        }

        public List<Sporter> GetAlleSporters()
        {
            if (_queue.Count == 0) return new List<Sporter>();

            List<Sporter> sporters = new List<Sporter>();

            foreach (Sporter sporter in _queue)
            {
                sporters.Add(sporter);
            }

            return sporters;
        }

        public List<Sporter> SportersVerlatenRij(int aantal)
        {
            if (_queue.Count == 0) return new List<Sporter>();

            List<Sporter> sporters = new List<Sporter>();

            int amount = aantal < _queue.Count ? aantal : aantal - _queue.Count;

            for (int i = 0; i < amount; i++)
            {
                sporters.Add(_queue.Dequeue());
            }

            return sporters;
        }
    }
}
