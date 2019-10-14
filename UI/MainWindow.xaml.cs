﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;
using Waterskibaan;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly DispatcherTimer _dispatcherTimer;
        private readonly Game _game;
        private readonly List<Sporter> _newVisitors = new List<Sporter>();
        private readonly List<Sporter> _newAthletes = new List<Sporter>();
        private readonly List<Sporter> _finishedAthletes = new List<Sporter>();
        private LinkedList<Lijn> _lines = new LinkedList<Lijn>();

        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();

            _game = new Game();

            _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };

            _game.NieuweBezoeker += OnNewVisitor;
            _game.InstructieAfgelopen += OnInstructionFinished;
            _game.LijnenVerplaats += OnChangeLines;

            _dispatcherTimer.Tick += (source, args) => canvas.Children.Clear();
            _dispatcherTimer.Tick += DrawGame;

            _game.Initialize(_dispatcherTimer);
            _dispatcherTimer.Start();
        }

        private void OnChangeLines(LijnenVerplaatsArgs e)
        {
            _finishedAthletes.Remove(e.Sporter);
            _lines = e.Lijnen;
        }

        private void OnInstructionFinished(InstructieAfgelopenArgs e)
        {
            foreach (var athlete in e.SportersNieuw)
            {
                _newVisitors.Remove(athlete);
                _newAthletes.Add(athlete);
            }

            foreach (var athlete in e.SportersKlaar)
            {
                _newAthletes.Remove(athlete);
                _finishedAthletes.Add(athlete);
            }
        }

        private void OnNewVisitor(NieuweBezoekerArgs e)
        {
            _newVisitors.Add(e.Sporter);
        }

        private static Rectangle CreateDrawableAthlete(Sporter athlete)
        {
            var converter = new BrushConverter();
            var fillBrush = (SolidColorBrush) converter.ConvertFromString(athlete.KledingKleur);
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

        private void DrawGame(object sender, EventArgs e)
        {
            DrawGround();
            DrawInstructionQueue();
            DrawInstructionGroup();
            DrawReadyToStartQueue();
            DrawWaterSkiLanes();
            
            label.Content = _game.ToString();
        }

        private void DrawGround()
        {
            var left = new Rectangle()
            {
                Width = 400,
                Height = canvas.Height,
                Stroke = new SolidColorBrush(Colors.Green),
                Fill = new SolidColorBrush(Colors.Green)
            };

            Canvas.SetLeft(left, 0);
            Canvas.SetTop(left, 0);
            canvas.Children.Add(left);

            var bottom = new Rectangle()
            {
                Width = canvas.Width,
                Height = 200,
                Stroke = new SolidColorBrush(Colors.Green),
                Fill = new SolidColorBrush(Colors.Green)
            };

            Canvas.SetLeft(bottom, 0);
            Canvas.SetBottom(bottom, 0);
            canvas.Children.Add(bottom);
        }

        private void bt_start_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Start();
        }

        private void bt_stop_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Stop();
        }

        private void DrawInstructionQueue()
        {
            var fence = new Line()
            {
                Height = canvas.Height,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = canvas.Height
            };

            Canvas.SetLeft(fence, 0);
            Canvas.SetTop(fence, 0);
            canvas.Children.Add(fence);

            for (var i = 0; i < 5; i++)
            {
                var line = new Line()
                {
                    Height = canvas.Height,
                    Fill = new SolidColorBrush(Colors.Black),
                    Stroke = new SolidColorBrush(Colors.Black),
                    X1 = 0,
                    X2 = 0,
                    Y1 = 0,
                    Y2 = canvas.Height - 30
                };

                Canvas.SetLeft(line, i * 30 + 30);

                Canvas.SetTop(line, i % 2 == 0 ? 30 : 0);

                canvas.Children.Add(line);
            }

            for (var i = 0; i < _newVisitors.Count; i++)
            {
                var sp = CreateDrawableAthlete(_newVisitors[i]);

                if (i < 20)
                {
                    Canvas.SetTop(sp, 10 + (32 * i));
                    Canvas.SetLeft(sp, 125);
                }
                else if (i >= 20 && i < 40)
                {
                    Canvas.SetTop(sp, canvas.Height - 25 - (32 * (i - 20)));
                    Canvas.SetLeft(sp, 95);
                }
                else if (i >= 40 && i < 60)
                {
                    Canvas.SetTop(sp, 10 + (32 * (i - 40)));
                    Canvas.SetLeft(sp, 65);
                }
                else if (i >= 60 && i < 80)
                {
                    Canvas.SetTop(sp, canvas.Height - 25 - (32 * (i - 60)));
                    Canvas.SetLeft(sp, 35);
                }
                else
                {
                    Canvas.SetTop(sp, 10 + (32 * (i - 80)));
                    Canvas.SetLeft(sp, 5);
                }

                canvas.Children.Add(sp);
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
            canvas.Children.Add(ground);

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
            Canvas.SetLeft(instructor, canvas.Width - 510);
            canvas.Children.Add(instructor);

            for (var i = 0; i < 5; i++)
            {
                var athlete = _newAthletes.Count >= i + 1 ? _newAthletes[i] : null;

                if (athlete == null) continue;

                var place = CreateDrawableAthlete(athlete);

                if (i >= 1 && i <= 3)
                {
                    Canvas.SetTop(place, 100);
                    Canvas.SetLeft(place, canvas.Width - 555 + (i * 22));
                    canvas.Children.Add(place);
                }
                else
                {
                    Canvas.SetTop(place, 80);
                    Canvas.SetLeft(place, canvas.Width - 555 + (i * 22));
                    canvas.Children.Add(place);
                }
            }
        }

        private void DrawReadyToStartQueue()
        {
            var bottomFence = new Line()
            {
                Height = canvas.Height,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
                X1 = 0,
                X2 = 60,
                Y1 = 0,
                Y2 = 0
            };

            Canvas.SetTop(bottomFence, 505);
            Canvas.SetRight(bottomFence, 380);
            canvas.Children.Add(bottomFence);

            var rightFence = new Line()
            {
                Height = canvas.Height,
                Fill = new SolidColorBrush(Colors.Black),
                Stroke = new SolidColorBrush(Colors.Black),
                X1 = 0,
                X2 = 0,
                Y1 = 0,
                Y2 = 275
            };

            Canvas.SetTop(rightFence, 230);
            Canvas.SetRight(rightFence, 380);
            canvas.Children.Add(rightFence);

            for (var i = 0; i < 2; i++)
            {
                var fence = new Line()
                {
                    Height = canvas.Height,
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

                canvas.Children.Add(fence);
            }

            for (var i = 0; i < _finishedAthletes.Count; i++)
            {
                var sp = CreateDrawableAthlete(_finishedAthletes[i]);

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

                canvas.Children.Add(sp);
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
                        Canvas.SetLeft(ellipse, canvas.Width - 300 + (i * 100));

                        Canvas.SetTop(number, 40);
                        Canvas.SetLeft(number, canvas.Width - 270 + (i * 100));
                        break;
                    case 3:
                    case 9:
                        Canvas.SetTop(ellipse, 140);
                        Canvas.SetLeft(ellipse, canvas.Width - 300 + (i == 9 ? 0 : 2 * 100));

                        Canvas.SetTop(number, 140);
                        Canvas.SetLeft(number, canvas.Width - 270 + (i == 9 ? 0 : 2 * 100));
                        break;
                    case 4:
                    case 8:
                        Canvas.SetTop(ellipse, 240);
                        Canvas.SetLeft(ellipse, canvas.Width - 300 + (i == 8 ? 0 : 2 * 100));

                        Canvas.SetTop(number, 240);
                        Canvas.SetLeft(number, canvas.Width - 270 + (i == 8 ? 0 : 2 * 100));
                        break;
                    default:
                        Canvas.SetTop(ellipse, 340);
                        Canvas.SetLeft(ellipse, canvas.Width - (100 + ((i - 5) * 100)));

                        Canvas.SetTop(number, 340);
                        Canvas.SetLeft(number, canvas.Width - (70 + ((i - 5) * 100)));
                        break;
                }

                canvas.Children.Add(ellipse);
                canvas.Children.Add(number);

                foreach (var line in _lines)
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
                            Canvas.SetLeft(athlete, canvas.Width - 300);

                            if (move != null)
                            {
                                Canvas.SetTop(move, 20);
                                Canvas.SetLeft(move, canvas.Width - 300);
                            }
                            break;
                        case 1:
                            Canvas.SetTop(athlete, 40);
                            Canvas.SetLeft(athlete, canvas.Width - 200);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 20);
                                Canvas.SetLeft(move, canvas.Width - 200);
                            }
                            break;
                        case 2:
                            Canvas.SetTop(athlete, 40);
                            Canvas.SetLeft(athlete, canvas.Width - 100);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 20);
                                Canvas.SetLeft(move, canvas.Width - 100);
                            }
                            break;
                        case 3:
                            Canvas.SetTop(athlete, 140);
                            Canvas.SetLeft(athlete, canvas.Width - 100);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 120);
                                Canvas.SetLeft(move, canvas.Width - 100);
                            }
                            break;
                        case 4:
                            Canvas.SetTop(athlete, 240);
                            Canvas.SetLeft(athlete, canvas.Width - 100);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 220);
                                Canvas.SetLeft(move, canvas.Width - 100);
                            }
                            break;
                        case 5:
                            Canvas.SetTop(athlete, 340);
                            Canvas.SetLeft(athlete, canvas.Width - 100);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 320);
                                Canvas.SetLeft(move, canvas.Width - 100);
                            }
                            break;
                        case 6:
                            Canvas.SetTop(athlete, 340);
                            Canvas.SetLeft(athlete, canvas.Width - 200);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 320);
                                Canvas.SetLeft(move, canvas.Width - 200);
                            }
                            break;
                        case 7:
                            Canvas.SetTop(athlete, 340);
                            Canvas.SetLeft(athlete, canvas.Width - 300);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 320);
                                Canvas.SetLeft(move, canvas.Width - 300);
                            }
                            break;
                        case 8:
                            Canvas.SetTop(athlete, 240);
                            Canvas.SetLeft(athlete, canvas.Width - 300);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 220);
                                Canvas.SetLeft(move, canvas.Width - 300);
                            }
                            break;
                        case 9:
                            Canvas.SetTop(athlete, 140);
                            Canvas.SetLeft(athlete, canvas.Width - 300);
                            
                            if (move != null)
                            {
                                Canvas.SetTop(move, 120);
                                Canvas.SetLeft(move, canvas.Width - 300);
                            }
                            break;
                    }

                    canvas.Children.Add(athlete);
                    if (move != null)
                    {
                        canvas.Children.Add(move);
                    }
                }
            }
        }
    }
}