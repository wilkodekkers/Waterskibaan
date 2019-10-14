using System;
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
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer DispatcherTimer;
        private readonly Game Game;
        private readonly List<Sporter> NewVisitors = new List<Sporter>();
        private readonly List<Sporter> NewSporters = new List<Sporter>();
        private readonly List<Sporter> FinishedSporters = new List<Sporter>();

        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();

            Game = new Game();

            DispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(35)
            };

            Game.NieuweBezoeker += OnNieuweBezoeker;
            Game.InstructieAfgelopen += OnInstructieAfgelopen;
            Game.LijnenVerplaats += OnLijnenVerplaats;

            DispatcherTimer.Tick += (source, args) => canvas.Children.Clear();
            DispatcherTimer.Tick += DrawGame;

            Game.Initialize(DispatcherTimer);
            DispatcherTimer.Start();
        }

        private void OnLijnenVerplaats(LijnenVerplaatsArgs e)
        {
            FinishedSporters.Remove(e.Sporter);
        }

        private void OnInstructieAfgelopen(InstructieAfgelopenArgs e)
        {
            foreach (Sporter sporter in e.SportersNieuw)
            {
                NewVisitors.Remove(sporter);
                NewSporters.Add(sporter);
            }

            foreach (Sporter sporter in e.SportersKlaar)
            {
                NewSporters.Remove(sporter);
                FinishedSporters.Add(sporter);
            }
        }

        private void OnNieuweBezoeker(NieuweBezoekerArgs e)
        {
            NewVisitors.Add(e.Sporter);
        }

        private Rectangle CreateSporterRectangle(Sporter sporter)
        {
            var converter = new BrushConverter();

            SolidColorBrush fillBrush = (SolidColorBrush)converter.ConvertFromString(sporter.KledingKleur);
            SolidColorBrush strokeBrush = new SolidColorBrush(Colors.Black);

            Rectangle sp = new Rectangle
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

            label.Content = Game.ToString();
        }


        private void DrawGround()
        {
            Rectangle left = new Rectangle()
            {
                Width = 400,
                Height = canvas.Height,
                Stroke = new SolidColorBrush(Colors.Green),
                Fill = new SolidColorBrush(Colors.Green)
            };

            Canvas.SetLeft(left, 0);
            Canvas.SetTop(left, 0);
            canvas.Children.Add(left);

            Rectangle bottom = new Rectangle()
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
            DispatcherTimer.Start();
        }

        private void bt_stop_Click(object sender, RoutedEventArgs e)
        {
            DispatcherTimer.Stop();
        }

        private void DrawInstructionQueue()
        {
            Line fence = new Line()
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

            for (int i = 0; i < 5; i++)
            {
                Line line = new Line()
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

                if (i % 2 == 0)
                {
                    Canvas.SetTop(line, 30);
                }
                else
                {
                    Canvas.SetTop(line, 0);
                }

                canvas.Children.Add(line);
            }

            for (int i = 0; i < NewVisitors.Count; i++)
            {
                Rectangle sp = CreateSporterRectangle(NewVisitors[i]);

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
            Rectangle ground = new Rectangle()
            {
                Stroke = new SolidColorBrush(Colors.SandyBrown),
                Fill = new SolidColorBrush(Colors.SandyBrown),
                Width = 250,
                Height = 200
            };

            Canvas.SetTop(ground, 0);
            Canvas.SetLeft(ground, 150);
            canvas.Children.Add(ground);

            SolidColorBrush blackBrush = new SolidColorBrush(Colors.Black);

            Rectangle instructor = new Rectangle()
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

            for (int i = 0; i < 5; i++)
            {
                Sporter sporter = NewSporters.Count >= i + 1 ? NewSporters[i] : null;

                if (sporter != null)
                {
                    Rectangle place = CreateSporterRectangle(sporter);

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
        }

        private void DrawReadyToStartQueue()
        {
            Line bottomFence = new Line()
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

            Line rightFence = new Line()
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

            for (int i = 0; i < 2; i++)
            {
                Line fence = new Line()
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

            for (int i = 0; i < FinishedSporters.Count; i++)
            {
                Rectangle sp = CreateSporterRectangle(FinishedSporters[i]);

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
            for (int i = 0; i < 10; i++)
            {
                Ellipse ellipse = new Ellipse()
                {
                    Stroke = new SolidColorBrush(Colors.Black),
                    Fill = new SolidColorBrush(Colors.White),
                    Width = 20,
                    Height = 20
                };

                TextBlock number = new TextBlock()
                {
                    Text = i.ToString(),
                    Foreground = new SolidColorBrush(Colors.Black)
                };

                if (i >= 0 && i <= 2)
                {
                    Canvas.SetTop(ellipse, 40);
                    Canvas.SetLeft(ellipse, canvas.Width - 300 + (i * 100));
                    canvas.Children.Add(ellipse);

                    Canvas.SetTop(number, 40);
                    Canvas.SetLeft(number, canvas.Width - 270 + (i * 100));
                    canvas.Children.Add(number);
                }
                else if (i == 3 || i == 9)
                {
                    Canvas.SetTop(ellipse, 140);
                    Canvas.SetLeft(ellipse, canvas.Width - 300 + (i == 9 ? 0 : 2 * 100));
                    canvas.Children.Add(ellipse);

                    Canvas.SetTop(number, 140);
                    Canvas.SetLeft(number, canvas.Width - 270 + (i == 9 ? 0 : 2 * 100));
                    canvas.Children.Add(number);
                }
                else if (i == 4 || i == 8)
                {
                    Canvas.SetTop(ellipse, 240);
                    Canvas.SetLeft(ellipse, canvas.Width - 300 + (i == 8 ? 0 : 2 * 100));
                    canvas.Children.Add(ellipse);

                    Canvas.SetTop(number, 240);
                    Canvas.SetLeft(number, canvas.Width - 270 + (i == 8 ? 0 : 2 * 100));
                    canvas.Children.Add(number);
                }
                else
                {
                    Canvas.SetTop(ellipse, 340);
                    Canvas.SetLeft(ellipse, canvas.Width - (100 + ((i - 5) * 100)));
                    canvas.Children.Add(ellipse);

                    Canvas.SetTop(number, 340);
                    Canvas.SetLeft(number, canvas.Width - (70 + ((i - 5) * 100)));
                    canvas.Children.Add(number);
                }
            }
        }
    }
}
