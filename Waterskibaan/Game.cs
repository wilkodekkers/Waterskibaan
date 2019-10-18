using System;
using System.Windows.Threading;

namespace Waterskibaan
{
    public class Game
    {
        public readonly Waterskibaan _waterskibaan = new Waterskibaan();
        private readonly WachtrijInstructie _wachtrijInstrucie = new WachtrijInstructie();
        private readonly InstructieGroep _instructieGroep = new InstructieGroep();
        private readonly WachtrijStarten _wachtrijStarten = new WachtrijStarten();
        private int _timeElapsed;

        public event Action<NieuweBezoekerArgs> NieuweBezoeker;
        public event Action<InstructieAfgelopenArgs> InstructieAfgelopen;
        public event Action<LijnenVerplaatsArgs> LijnenVerplaats;
        public readonly Logger logger;

        public Game()
        {
            logger = new Logger(_waterskibaan.Kabel);
        }

        public void Initialize(DispatcherTimer timer)
        {
            NieuweBezoeker += _wachtrijInstrucie.OnNieuweBezoeker;
            InstructieAfgelopen += _instructieGroep.OnInstructieAfgelopen;
            InstructieAfgelopen += _wachtrijStarten.OnInstructieAfgelopen;

            timer.Tick += GameLoop;
        }

        private void GameLoop(object source, EventArgs args)
        {
            _timeElapsed++;

            if (_timeElapsed % 3 == 0)
            {
                HandleNewVisitor();
            }

            if (_timeElapsed % 4 == 0)
            {
                HandleChangeLines();
            }

            if (_timeElapsed % 20 == 0)
            {
                HandleInstructionEnded();
            }
        }

        private void HandleNewVisitor()
        {
            var visitor = new Sporter(MoveCollection.GetWillekeurigeMoves());
            var args = new NieuweBezoekerArgs()
            {
                Sporter = visitor
            };

            logger.AddVisitor(visitor);
            NieuweBezoeker?.Invoke(args);
        }

        private void HandleInstructionEnded()
        {
            var args = new InstructieAfgelopenArgs
            {
                SportersKlaar = _instructieGroep.SportersVerlatenRij(5),
                SportersNieuw = _wachtrijInstrucie.SportersVerlatenRij(5)
            };

            InstructieAfgelopen?.Invoke(args);
        }

        private void HandleChangeLines()
        {
            _waterskibaan.VerplaatsKabel();

            if (_wachtrijStarten.GetAlleSporters().Count > 0 && _waterskibaan.Kabel.IsStartPositieLeeg() != false)
            {
                var athlete = _wachtrijStarten.SportersVerlatenRij(1)[0];
                var random = new Random();

                athlete.Skies = new Skies();
                athlete.Zwemvest = new Zwemvest();

                _waterskibaan.SporterStart(athlete);

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
}