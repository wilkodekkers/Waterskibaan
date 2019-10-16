using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Waterskibaan
{
    public class Logger
    {
        private List<Sporter> Visitors { get; }
        public Kabel Kabel { get; set; }

        public Logger()
        {
            Visitors = new List<Sporter>();
        }

        public void AddVisitor(Sporter visitor)
        {
            Visitors.Add(visitor);
        }

        public int GetAmountOfVisitors()
        {
            return Visitors.Count;
        }

        public int GetHighestScore()
        {
            return Visitors.Count > 0 ? Visitors.Max(visitor => visitor.Score) : 0;
        }

        public int GetAmountOfAthletesWithRedClothes()
        {
            return Visitors
                .Where(v => ColorsAreClose(Color.FromRgb(v.KledingKleur.Item1, v.KledingKleur.Item2, v.KledingKleur.Item1), Colors.Red, 190))
                .ToList().Count;
        }

        public List<Color> GetListWithLightestClothes()
        {
            return Visitors
                .OrderByDescending(a => CalculateColorValue(Color.FromRgb(a.KledingKleur.Item1, a.KledingKleur.Item2, a.KledingKleur.Item3)))
                .Take(10)
                .Select(a => Color.FromRgb(a.KledingKleur.Item1, a.KledingKleur.Item2, a.KledingKleur.Item3))
                .ToList();
        }

        public int GetAmountOfLaps()
        {
            return Visitors
                .Select(visitor => visitor.AantalRondenNogTeGaan)
                .Sum();
        }

        public List<IMoves> GetUniqueMoves()
        {
            return Kabel.Lijnen
                .SelectMany(l => l.Sporter.Moves)
                .Distinct()
                .ToList();
        }

        private bool ColorsAreClose(Color a, Color z, int threshold)
        {
            int r = a.R - z.R;
            int g = a.G - z.G;
            int b = a.B - z.B;

            return (r * r + g * g + b * b) <= threshold * threshold;
        }

        private int CalculateColorValue(Color color)
        {
            return (color.R * color.R + color.G * color.G + color.B * color.B);
        }
    }
}