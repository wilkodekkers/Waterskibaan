using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Waterskibaan;

namespace UI
{
    class CanvasRenderer
    {
        private Canvas Canvas { get; set; }
        private List<Sporter> NewVisitors { get; set; } = new List<Sporter>();
        private List<Sporter> NewAthletes { get; set; } = new List<Sporter>();
        private List<Sporter> FinishedAthletes { get; set; } = new List<Sporter>();
        private LinkedList<Lijn> Lines { get; set; } = new LinkedList<Lijn>();

        public CanvasRenderer(Canvas canvas, Game game)
        {
            Canvas = canvas;

            game.NieuweBezoeker += OnNewVisitor;
            game.InstructieAfgelopen += OnInstructionFinished;
            game.LijnenVerplaats += OnChangeLines;
        }

        private void OnNewVisitor(NieuweBezoekerArgs args)
        {
            NewVisitors.Add(args.Sporter);
        }

        private void OnInstructionFinished(InstructieAfgelopenArgs args)
        {
            foreach (var athlete in args.SportersNieuw)
            {
                NewVisitors.Remove(athlete);
                NewAthletes.Add(athlete);
            }

            foreach (var athlete in args.SportersKlaar)
            {
                NewAthletes.Remove(athlete);
                FinishedAthletes.Add(athlete);
            }
        }

        private void OnChangeLines(LijnenVerplaatsArgs args)
        {
            FinishedAthletes.Remove(args.Sporter);
            Lines = args.Lijnen;
        }

        private static Rectangle CreateDrawableAthlete(Sporter athlete)
        {
            var fillBrush = new SolidColorBrush(Color.FromRgb(athlete.KledingKleur.Item1, athlete.KledingKleur.Item2, athlete.KledingKleur.Item3));
            var strokeBrush = new SolidColorBrush(Colors.Black);
            var sp = new Rectangle
            {
                Stroke = strokeBrush,
                Fill = fillBrush,
                Height = 20,
                Width = 20,
                RadiusX = 5,
                RadiusY = 5
            };

            return sp;
        }

        public void Render()
        {
            DrawGround();
            DrawInstructionQueue();
            DrawInstructionGroup();
            DrawReadyToStartQueue();
            DrawWaterSkiLanes();
        }

        private void DrawGround()
        {
            var left = new Rectangle()
            {
                Width = 400,
                Height = Canvas.Height,
                Stroke = new SolidColorBrush(Colors.Green),
                Fill = new SolidColorBrush(Colors.Green)
            };

            Canvas.SetLeft(left, 0);
            Canvas.SetTop(left, 0);
            Canvas.Children.Add(left);

            var bottom = new Rectangle()
            {
                Width = Canvas.Width,
                Height = 200,
                Stroke = new SolidColorBrush(Colors.Green),
                Fill = new SolidColorBrush(Colors.Green)
            };

            Canvas.SetLeft(bottom, 0);
            Canvas.SetBottom(bottom, 0);
            Canvas.Children.Add(bottom);
        }

        private void DrawInstructionQueue()
        {
            var fence = new Line()
            {
                Height = Canvas.Height,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = Canvas.Height
            };

            Canvas.SetLeft(fence, 0);
            Canvas.SetTop(fence, 0);
            Canvas.Children.Add(fence);

            for (var i = 0; i < 5; i++)
            {
                var line = new Line()
                {
                    Height = Canvas.Height,
                    Fill = new SolidColorBrush(Colors.Black),
                    Stroke = new SolidColorBrush(Colors.Black),
                    X1 = 0,
                    X2 = 0,
                    Y1 = 0,
                    Y2 = Canvas.Height - 30
                };

                Canvas.SetLeft(line, i * 30 + 30);

                Canvas.SetTop(line, i % 2 == 0 ? 30 : 0);

                Canvas.Children.Add(line);
            }

            for (var i = 0; i < NewVisitors.Count; i++)
            {
                var sp = CreateDrawableAthlete(NewVisitors[i]);

                if (i < 20)
                {
                    Canvas.SetTop(sp, 10 + (32 * i));
                    Canvas.SetLeft(sp, 125);
                }
                else if (i >= 20 && i < 40)
                {
                    Canvas.SetTop(sp, Canvas.Height - 25 - (32 * (i - 20)));
                    Canvas.SetLeft(sp, 95);
                }
                else if (i >= 40 && i < 60)
                {
                    Canvas.SetTop(sp, 10 + (32 * (i - 40)));
                    Canvas.SetLeft(sp, 65);
                }
                else if (i >= 60 && i < 80)
                {
                    Canvas.SetTop(sp, Canvas.Height - 25 - (32 * (i - 60)));
                    Canvas.SetLeft(sp, 35);
                }
                else
                {
                    Canvas.SetTop(sp, 10 + (32 * (i - 80)));
                    Canvas.SetLeft(sp, 5);
                }

                Canvas.Children.Add(sp);
            }
        }

        private void DrawInstructionGroup()
        {
            var ground = new Rectangle()
            {
                Stroke = new SolidColorBrush(Colors.SandyBrown),
                Fill = new SolidColorBrush(Colors.SandyBrown),
                Width = 250,
                Height = 200
            };

            Canvas.SetTop(ground, 0);
            Canvas.SetLeft(ground, 150);
            Canvas.Children.Add(ground);

            var blackBrush = new SolidColorBrush(Colors.Black);

            var instructor = new Rectangle()
            {
                Stroke = blackBrush,
                Fill = new SolidColorBrush(Colors.White),
                Width = 20,
                Height = 20,
                RadiusX = 5,
                RadiusY = 5
            };

            Canvas.SetTop(instructor, 60);
            Canvas.SetLeft(instructor, Canvas.Width - 510);
            Canvas.Children.Add(instructor);

            for (var i = 0; i < 5; i++)
            {
                var athlete = NewAthletes.Count >= i + 1 ? NewAthletes[i] : null;

                if (athlete == null) continue;

                var place = CreateDrawableAthlete(athlete);

                if (i >= 1 && i <= 3)
                {
                    Canvas.SetTop(place, 100);
                    Canvas.SetLeft(place, Canvas.Width - 555 + (i * 22));
                    Canvas.Children.Add(place);
                }
                else
                {
                    Canvas.SetTop(place, 80);
                    Canvas.SetLeft(place, Canvas.Width - 555 + (i * 22));
                    Canvas.Children.Add(place);
                }
            }
        }

        private void DrawReadyToStartQueue()
        {
            var bottomFence = new Line()
            {
                Height = Canvas.Height,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
                X1 = 0,
                X2 = 60,
                Y1 = 0,
                Y2 = 0
            };

            Canvas.SetTop(bottomFence, 505);
            Canvas.SetRight(bottomFence, 380);
            Canvas.Children.Add(bottomFence);

            var rightFence = new Line()
            {
                Height = Canvas.Height,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = 275
            };

            Canvas.SetTop(rightFence, 230);
            Canvas.SetRight(rightFence, 380);
            Canvas.Children.Add(rightFence);

            for (var i = 0; i < 2; i++)
            {
                var fence = new Line()
                {
                    Height = Canvas.Height,
                    Fill = new SolidColorBrush(Colors.Black),
                    Stroke = new SolidColorBrush(Colors.Black),
                    X1 = 0,
                    X2 = 0,
                    Y1 = 0,
                    Y2 = 305
                };

                Canvas.SetTop(fence, 200);

                if (i == 0)
                {
                    Canvas.SetRight(fence, 440);
                }
                else
                {
                    Canvas.SetRight(fence, 410);
                    fence.Y2 = 275;
                }

                Canvas.Children.Add(fence);
            }

            for (var i = 0; i < FinishedAthletes.Count; i++)
            {
                var sp = CreateDrawableAthlete(FinishedAthletes[i]);

                if (i < 10)
                {
                    Canvas.SetTop(sp, 210 + (i * 30));
                    Canvas.SetRight(sp, 385);
                }
                else
                {
                    Canvas.SetTop(sp, 480);
                    Canvas.SetRight(sp, 415);
                }

                Canvas.Children.Add(sp);
            }
        }

        private void DrawWaterSkiLanes()
        {
            for (var i = 0; i < 10; i++)
            {
                var ellipse = new Ellipse()
                {
                    Stroke = new SolidColorBrush(Colors.Black),
                    Fill = new SolidColorBrush(Colors.White),
                    Width = 20,
                    Height = 20
                };

                var number = new TextBlock()
                {
                    Text = i.ToString(),
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                        Canvas.SetTop(ellipse, 40);
                        Canvas.SetLeft(ellipse, Canvas.Width - 300 + (i * 100));

                        Canvas.SetTop(number, 40);
                        Canvas.SetLeft(number, Canvas.Width - 270 + (i * 100));
                        break;
                    case 3:
                    case 9:
                        Canvas.SetTop(ellipse, 140);
                        Canvas.SetLeft(ellipse, Canvas.Width - 300 + (i == 9 ? 0 : 2 * 100));

                        Canvas.SetTop(number, 140);
                        Canvas.SetLeft(number, Canvas.Width - 270 + (i == 9 ? 0 : 2 * 100));
                        break;
                    case 4:
                    case 8:
                        Canvas.SetTop(ellipse, 240);
                        Canvas.SetLeft(ellipse, Canvas.Width - 300 + (i == 8 ? 0 : 2 * 100));

                        Canvas.SetTop(number, 240);
                        Canvas.SetLeft(number, Canvas.Width - 270 + (i == 8 ? 0 : 2 * 100));
                        break;
                    default:
                        Canvas.SetTop(ellipse, 340);
                        Canvas.SetLeft(ellipse, Canvas.Width - (100 + ((i - 5) * 100)));

                        Canvas.SetTop(number, 340);
                        Canvas.SetLeft(number, Canvas.Width - (70 + ((i - 5) * 100)));
                        break;
                }

                Canvas.Children.Add(ellipse);
                Canvas.Children.Add(number);

                foreach (var line in Lines)
                {
                    var athlete = CreateDrawableAthlete(line.Sporter);

                    TextBlock move = null;

                    if (line.Sporter.HuidigeMove != null)
                    {
                        move = new TextBlock()
                        {
                            Text = line.Sporter.HuidigeMove.ToString(),
                            Foreground = new SolidColorBrush(Colors.Black)
                        };
                    }

                    switch (line.PositieOpDeKabel)
                    {
                        case 0:
                            Canvas.SetTop(athlete, 40);
                            Canvas.SetLeft(athlete, Canvas.Width - 300);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 20);
                                Canvas.SetLeft(move, Canvas.Width - 300);
                            }
                            break;
                        case 1:
                            Canvas.SetTop(athlete, 40);
                            Canvas.SetLeft(athlete, Canvas.Width - 200);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 20);
                                Canvas.SetLeft(move, Canvas.Width - 200);
                            }
                            break;
                        case 2:
                            Canvas.SetTop(athlete, 40);
                            Canvas.SetLeft(athlete, Canvas.Width - 100);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 20);
                                Canvas.SetLeft(move, Canvas.Width - 100);
                            }
                            break;
                        case 3:
                            Canvas.SetTop(athlete, 140);
                            Canvas.SetLeft(athlete, Canvas.Width - 100);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 120);
                                Canvas.SetLeft(move, Canvas.Width - 100);
                            }
                            break;
                        case 4:
                            Canvas.SetTop(athlete, 240);
                            Canvas.SetLeft(athlete, Canvas.Width - 100);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 220);
                                Canvas.SetLeft(move, Canvas.Width - 100);
                            }
                            break;
                        case 5:
                            Canvas.SetTop(athlete, 340);
                            Canvas.SetLeft(athlete, Canvas.Width - 100);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 320);
                                Canvas.SetLeft(move, Canvas.Width - 100);
                            }
                            break;
                        case 6:
                            Canvas.SetTop(athlete, 340);
                            Canvas.SetLeft(athlete, Canvas.Width - 200);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 320);
                                Canvas.SetLeft(move, Canvas.Width - 200);
                            }
                            break;
                        case 7:
                            Canvas.SetTop(athlete, 340);
                            Canvas.SetLeft(athlete, Canvas.Width - 300);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 320);
                                Canvas.SetLeft(move, Canvas.Width - 300);
                            }
                            break;
                        case 8:
                            Canvas.SetTop(athlete, 240);
                            Canvas.SetLeft(athlete, Canvas.Width - 300);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 220);
                                Canvas.SetLeft(move, Canvas.Width - 300);
                            }
                            break;
                        case 9:
                            Canvas.SetTop(athlete, 140);
                            Canvas.SetLeft(athlete, Canvas.Width - 300);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 120);
                                Canvas.SetLeft(move, Canvas.Width - 300);
                            }
                            break;
                    }

                    Canvas.Children.Add(athlete);
                    if (move != null)
                    {
                        Canvas.Children.Add(move);
                    }
                }
            }
        }
    }
}
