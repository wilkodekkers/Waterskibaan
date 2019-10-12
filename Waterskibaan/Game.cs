using System;
using System.Windows.Threading;

namespace Waterskibaan
{
    public class Game
    {
        private readonly Waterskibaan _waterskibaan = new Waterskibaan();
        private readonly WachtrijInstructie _wachtrijInstrucie = new WachtrijInstructie();
        private readonly InstructieGroep _instructieGroep = new InstructieGroep();
        private readonly WachtrijStarten _wachtrijStarten = new WachtrijStarten();

        public event Action<NieuweBezoekerArgs> NieuweBezoeker;
        public event Action<InstructieAfgelopenArgs> InstructieAfgelopen;
        public event Action<LijnenVerplaatsArgs> LijnenVerplaats;
        public int TimeElapsed;

        public void Initialize(DispatcherTimer timer)
        {
            NieuweBezoeker += _wachtrijInstrucie.OnNieuweBezoeker;
            InstructieAfgelopen += _instructieGroep.OnInstructieAfgelopen;
            InstructieAfgelopen += _wachtrijStarten.OnInstructieAfgelopen;

            timer.Tick += OnTimerElapsed;
            timer.Tick += OnNieuweBezoeker;
            timer.Tick += OnInstructieAfgelopen;
            timer.Tick += OnLijnenVerplaats;
        }

        private void OnTimerElapsed(object source, EventArgs e)
        {
            TimeElapsed++;

            Console.WriteLine(_waterskibaan);
            Console.WriteLine(_wachtrijInstrucie);
            Console.WriteLine(_instructieGroep);
            Console.WriteLine(_wachtrijStarten);
        }

        private void OnNieuweBezoeker(object source, EventArgs e)
        {
            if (TimeElapsed % 3 != 0) return;

            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());
            NieuweBezoekerArgs args = new NieuweBezoekerArgs
            {
                Sporter = sporter
            };

            NieuweBezoeker?.Invoke(args);
        }

        private void OnInstructieAfgelopen(object source, EventArgs e)
        {
            if (TimeElapsed % 20 != 0) return;

            InstructieAfgelopenArgs args = new InstructieAfgelopenArgs
            {
                SportersKlaar = _instructieGroep.SportersVerlatenRij(5),
                SportersNieuw = _wachtrijInstrucie.SportersVerlatenRij(5)
            };

            InstructieAfgelopen?.Invoke(args);
        }

        private void OnLijnenVerplaats(object source, EventArgs e)
        {
            if (TimeElapsed % 4 != 0) return;

            _waterskibaan.VerplaatsKabel();

            if (_wachtrijStarten.GetAlleSporters().Count == 0) return;

            if (!_waterskibaan.Kabel.IsStartPositieLeeg()) return;

            Sporter sporter = _wachtrijStarten.SportersVerlatenRij(1)[0];
            sporter.Skies = new Skies();
            sporter.Zwemvest = new Zwemvest();

            _waterskibaan.SporterStart(sporter);

            LijnenVerplaatsArgs args = new LijnenVerplaatsArgs
            {
                Sporter = sporter
            };

            LijnenVerplaats?.Invoke(args);
        }

        public override string ToString()
        {
            return $"Waterskibaan \n\n {_waterskibaan} \n {_wachtrijInstrucie} \n {_instructieGroep} \n {_wachtrijStarten}";
        }
    }
}
