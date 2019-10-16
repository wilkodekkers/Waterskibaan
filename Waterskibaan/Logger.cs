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
            if (Visitors.Count == 0) return 0;
            return Visitors.Max(visitor => visitor.Score);
        }

        public int GetAmountOfAthletesWithRedClothes()
        {
            return Visitors.Where(visitor =>
            {
                Color color = Color.FromRgb(visitor.KledingKleur.Item1, visitor.KledingKleur.Item2, visitor.KledingKleur.Item3);
                return ColorsAreClose(color, Colors.Red, 150);
            }).ToList().Count;
        }

        public List<Color> GetListWithLightestClothes()
        {
            List<Color> colors = new List<Color>();

            IEnumerable<Sporter> list = Visitors.OrderByDescending(a =>
            {
                var color = Color.FromRgb(a.KledingKleur.Item1, a.KledingKleur.Item2, a.KledingKleur.Item3);
                return (color.R * color.R + color.G * color.G + color.B * color.B);
            }).Take(10);

            foreach (Sporter sporter in list)
            {
                colors.Add(Color.FromRgb(sporter.KledingKleur.Item1, sporter.KledingKleur.Item2, sporter.KledingKleur.Item3));
            }

            return colors;
        }

        public int GetAmountOfLaps()
        {
            return Visitors.Select(visitor => visitor.AantalRondenNogTeGaan).Sum();
        }

        public List<string> GetUniqueMoves()
        {
            var uniqueMoves = new List<string>();
            Kabel.Lijnen.ToList().ForEach(line => line.Sporter.Moves.ForEach(move => uniqueMoves.Add(move.ToString())));
            return uniqueMoves.Distinct().ToList();
        }

        private bool ColorsAreClose(Color a, Color z, int threshold)
        {
            int r = a.R - z.R;
            int g = a.G - z.G;
            int b = a.B - z.B;

            return (r * r + g * g + b * b) <= threshold * threshold;
        }
    }
}