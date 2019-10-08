using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace Waterskibaan
{
    public class NieuweBezoekerArgs : EventArgs
    {
        public Sporter Sporter { get; set; }
    }

    public class InstructieAfgelopenArgs : EventArgs
    {
        public List<Sporter> SportersKlaar { get; set; }
        public List<Sporter> SportersNieuw { get; set; }
    }

    public class Game
    {
        private Timer _timer;
        private int _elapsed;

        private Waterskibaan _waterskibaan = new Waterskibaan();
        private WachtrijInstructie _wachtrijInstrucie;
        private InstructieGroep _instructieGroep;
        private WachtrijStarten _wachtrijStarten;

        public delegate void NieuweBezoekerHandler(NieuweBezoekerArgs args);
        public delegate void InstructieAfgelopenHandler(InstructieAfgelopenArgs args);
        public event NieuweBezoekerHandler NieuweBezoeker;
        public event InstructieAfgelopenHandler InstructieAfgelopen;


        public Game()
        {
            _wachtrijInstrucie = new WachtrijInstructie();
            _instructieGroep = new InstructieGroep();
            _wachtrijStarten = new WachtrijStarten();
        }

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
            _elapsed++;

            Console.WriteLine(_waterskibaan);
            Console.WriteLine(_wachtrijInstrucie);
            Console.WriteLine(_instructieGroep);
            Console.WriteLine(_wachtrijStarten);
        }

        private void OnNieuweBezoeker(object source, EventArgs e)
        {
            if (_elapsed % 3 != 0) return;

            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());
            NieuweBezoekerArgs args = new NieuweBezoekerArgs();
            args.Sporter = sporter;

            NieuweBezoeker?.Invoke(args);
        }

        private void OnInstructieAfgelopen(object source, EventArgs e)
        {
            if (_elapsed % 20 != 0) return;

            InstructieAfgelopenArgs args = new InstructieAfgelopenArgs();
            args.SportersKlaar = _instructieGroep.SportersVerlatenRij(5);
            args.SportersNieuw = _wachtrijInstrucie.SportersVerlatenRij(5);

            InstructieAfgelopen?.Invoke(args);
        }

        private void OnLijnenVerplaats(object source, EventArgs e)
        {
            if (_elapsed % 4 != 0) return;

            _waterskibaan.VerplaatsKabel();

            if (_wachtrijStarten.GetAlleSporters().Count == 0) return;

            Sporter sporter = _wachtrijStarten.SportersVerlatenRij(1)[0];
            sporter.Skies = new Skies();
            sporter.Zwemvest = new Zwemvest();

            _waterskibaan.SporterStart(sporter);
        }
    }
}
