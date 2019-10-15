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
        private readonly Logger _logger = new Logger();

        public event Action<NieuweBezoekerArgs> NieuweBezoeker;
        public event Action<InstructieAfgelopenArgs> InstructieAfgelopen;
        public event Action<LijnenVerplaatsArgs> LijnenVerplaats;
        private int _timeElapsed;

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

            _logger.Visitors.Add(visitor);

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

        private bool ColorsAreClose(Color a, Color z, int threshold)
        {
            int r = a.R - z.R;
            int g = a.G - z.G;
            int b = a.B - z.B;

            return (r * r + g * g + b * b) <= threshold * threshold;
        }

        public override string ToString()
        {
            var data = "Waterskibaan\n\n";

            data += $"{_waterskibaan}\n";
            data += $"{_wachtrijInstrucie}\n";
            data += $"{_instructieGroep}\n";
            data += $"{_wachtrijStarten}\n\n";

            data += $"Totaal aantal bezoekers: {_logger.Visitors.Count}\n";
            if (_logger.Visitors.Count > 0)
            {
                var laps = 0;
                var uniqueMoves = new List<string>();
                var amountOfRedClothing = 0;

                _logger.Visitors.ForEach(x => laps += Math.Abs(x.AantalRondenNogTeGaan));
                _waterskibaan.Kabel.Lijnen.ToList().ForEach(line => line.Sporter.Moves.ForEach(move => uniqueMoves.Add(move.ToString())));
                uniqueMoves = uniqueMoves.Distinct().ToList();

                data += $"Hoogste score: {_logger.Visitors.Max(x => x.Score)}\n";
                data += "Aantal bezoekers met rode kleding: " + String.Join("", _logger.Visitors.Where(s =>
                {
                    Color color = Color.FromRgb(s.KledingKleur.Item1, s.KledingKleur.Item2, s.KledingKleur.Item3);
                    return ColorsAreClose(color, Colors.Red, 100);
                }).ToList().Count);

                data += "\n";

                data += string.Join("", _logger.Visitors.OrderByDescending(a =>
                {
                    var color = Color.FromRgb(a.KledingKleur.Item1, a.KledingKleur.Item2, a.KledingKleur.Item3);
                    return (color.R * color.R + color.G * color.G + color.B * color.B);
                }).Take(10).Select(s => s.KledingKleur + "\n"));

                data += $"Totaal aantal rondjes: {laps}\n";
                data += $"Unieke moves: ";
                uniqueMoves.ForEach(move => data += $"\n - {move}");
            }
            else
            {
                data += "Hoogste score: 0";
                data += "Aantal bezoekers met rode kleding: 0";
                data += "Lichste kleding: []";
                data += "Totaal aantal rondjes: 0";
                data += "Unieke moves: []";
            }

            return data;
        }
    }
}