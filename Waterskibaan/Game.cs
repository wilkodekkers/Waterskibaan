using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Waterskibaan
{
    public class NieuweBezoekerArgs : EventArgs
    {
        private Sporter _sporter;

        public NieuweBezoekerArgs(Sporter sporter)
        {
            Sporter = sporter;
        }

        public Sporter Sporter { get => _sporter; set => _sporter = value; }
    }

    public class InstructieAfgelopenArgs : EventArgs
    {
        private List<Sporter> _sporters;

        public InstructieAfgelopenArgs(List<Sporter> sporters)
        {
            Sporters = sporters;
        }

        public List<Sporter> Sporters { get => _sporters; set => _sporters = value; }
    }

    public class Game
    {
        private Timer _timer;
        private int _elapsed;

        private Waterskibaan _waterskibaan = new Waterskibaan();
        private WachtrijInstructie _wachtrijInstrucie;
        private InstructieGroep _instructieGroep;
        private WachtrijStarten _wachtrijStarten = new WachtrijStarten();

        public delegate void NieuweBezoekerHandler(NieuweBezoekerArgs args);
        public delegate void InstructieAfgelopenHandler(InstructieAfgelopenArgs args);
        public event NieuweBezoekerHandler NieuweBezoeker;
        public event InstructieAfgelopenHandler InstructieAfgelopen;

        public Game()
        {
            _wachtrijInstrucie = new WachtrijInstructie(this);
            _instructieGroep = new InstructieGroep(this);
        }

        public void Initialize()
        {
            SetupTimer();

            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();

            _timer.Stop();
            _timer.Dispose();
        }

        private void SetupTimer()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Elapsed += OnNieuweBezoeker;
            _timer.Elapsed += OnInstructieAfgelopen;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            _elapsed++;

            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());
            sporter.Skies = new Skies();
            sporter.Zwemvest = new Zwemvest();

            _waterskibaan.SporterStart(sporter);
            _waterskibaan.VerplaatsKabel();

            Console.WriteLine(_waterskibaan);
        }

        private void OnNieuweBezoeker(object source, ElapsedEventArgs e)
        {
            if (_elapsed % 3 != 0) return;

            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());
            NieuweBezoekerArgs args = new NieuweBezoekerArgs(sporter);

            NieuweBezoeker?.Invoke(args);

            Console.WriteLine(_wachtrijInstrucie);
        }

        private void OnInstructieAfgelopen(object source, ElapsedEventArgs e)
        {
            if (_elapsed % 20 != 0) return;

            List<Sporter> sporters = _wachtrijInstrucie.SportersVerlatenRij(5);

            InstructieAfgelopenArgs args = new InstructieAfgelopenArgs(sporters);

            InstructieAfgelopen?.Invoke(args);

            Console.WriteLine(_instructieGroep);
        }
    }
}
