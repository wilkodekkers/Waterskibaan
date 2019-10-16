using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows.Media;

namespace Waterskibaan
{
    public class Game
    {
        private readonly Waterskibaan _waterskibaan = new Waterskibaan();
        private readonly WachtrijInstructie _wachtrijInstrucie = new WachtrijInstructie();
        private readonly InstructieGroep _instructieGroep = new InstructieGroep();
        private readonly WachtrijStarten _wachtrijStarten = new WachtrijStarten();
        public readonly Logger logger = new Logger();

        public event Action<NieuweBezoekerArgs> NieuweBezoeker;
        public event Action<InstructieAfgelopenArgs> InstructieAfgelopen;
        public event Action<LijnenVerplaatsArgs> LijnenVerplaats;
        private int _timeElapsed;

        public Game()
        {
            logger.Kabel = _waterskibaan.Kabel;
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
            _timeElapsed++;

            Console.WriteLine(_waterskibaan);
            Console.WriteLine(_wachtrijInstrucie);
            Console.WriteLine(_instructieGroep);
            Console.WriteLine(_wachtrijStarten);
        }

        private void OnNieuweBezoeker(object source, EventArgs e)
        {
            if (_timeElapsed % 3 != 0) return;

            var visitor = new Sporter(MoveCollection.GetWillekeurigeMoves());
            var args = new NieuweBezoekerArgs
            {
                Sporter = visitor
            };

            logger.AddVisitor(visitor);

            NieuweBezoeker?.Invoke(args);
        }

        private void OnInstructieAfgelopen(object source, EventArgs e)
        {
            if (_timeElapsed % 20 != 0) return;

            var args = new InstructieAfgelopenArgs
            {
                SportersKlaar = _instructieGroep.SportersVerlatenRij(5),
                SportersNieuw = _wachtrijInstrucie.SportersVerlatenRij(5)
            };

            InstructieAfgelopen?.Invoke(args);
        }

        private void OnLijnenVerplaats(object source, EventArgs e)
        {
            if (_timeElapsed % 4 != 0) return;

            _waterskibaan.VerplaatsKabel();

            if (_wachtrijStarten.GetAlleSporters().Count == 0) return;

            if (!_waterskibaan.Kabel.IsStartPositieLeeg()) return;

            var athlete = _wachtrijStarten.SportersVerlatenRij(1)[0];
            athlete.Skies = new Skies();
            athlete.Zwemvest = new Zwemvest();

            _waterskibaan.SporterStart(athlete);

            var random = new Random();

            foreach (var line in _waterskibaan.Kabel.Lijnen)
            {
                line.Sporter.HuidigeMove = random.Next(0, 100) <= 25 ? line.Sporter.Moves[random.Next(0, line.Sporter.Moves.Count)] : null;
                line.Sporter.Score += line.Sporter.HuidigeMove?.Move() ?? 0;
            }

            var args = new LijnenVerplaatsArgs
            {
                Sporter = athlete,
                Lijnen = _waterskibaan.Kabel.Lijnen
            };

            LijnenVerplaats?.Invoke(args);
        }
    }
}