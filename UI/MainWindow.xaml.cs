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
        private readonly LinkedList<Sporter> FinishedSporters = new LinkedList<Sporter>();

        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();

            Game = new Game();

            DispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(200)
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
                FinishedSporters.AddLast(sporter);
            }
        }

        private void OnNieuweBezoeker(NieuweBezoekerArgs e)
        {
            NewVisitors.Add(e.Sporter);
        }

        private void DrawGame(object sender, EventArgs e)
        {
            DrawGround();
            DrawInstructionQueue();
            //DrawInstructionGroup();
            //DrawQueue(FinishedSporters, 2);
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

        private void DrawQueue(LinkedList<Sporter> queue, int offset)
        {
            Line fence = new Line
            {
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Black),
                X1 = 32 * offset,
                X2 = 32 * offset,
                Y1 = 0,
                Y2 = canvas.Height
            };

            canvas.Children.Add(fence);

            int count = 0;

            foreach (Sporter sporter in queue)
            {
                int heightOffset = 25 * count;

                DrawNewVisitor(sporter, heightOffset, offset);

                count++;
            }
        }

        private void DrawNewVisitor(Sporter sporter, int offset, int leftOffset)
        {
            var converter = new BrushConverter();

            SolidColorBrush fillBrush = (SolidColorBrush)converter.ConvertFromString(sporter.KledingKleur);
            SolidColorBrush strokeBrush = new SolidColorBrush(Colors.Black);

            Rectangle leftEllipse = new Rectangle
            {
                Stroke = strokeBrush,
                Fill = fillBrush,
                Height = 20,
                Width = 20,
                RadiusX = 5,
                RadiusY = 5
            };

            int setLeft = 5 + ((leftOffset - 1) * 32);

            Canvas.SetTop(leftEllipse, 5 + offset);
            Canvas.SetLeft(leftEllipse, setLeft);

            canvas.Children.Add(leftEllipse);
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
                var sporter = NewVisitors[i];
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

            Canvas.SetTop(instructor, canvas.Height - 30);
            Canvas.SetLeft(instructor, canvas.Width - 615);
            canvas.Children.Add(instructor);

            for (int i = 0; i < 5; i++)
            {
                Sporter sporter = NewSporters.Count >= i + 1 ? NewSporters[i] : null;

                if (sporter != null)
                {
                    var converter = new BrushConverter();

                    SolidColorBrush fillBrush = (SolidColorBrush)converter.ConvertFromString(sporter.KledingKleur);

                    Rectangle place = new Rectangle()
                    {
                        Stroke = blackBrush,
                        Fill = fillBrush,
                        Width = 20,
                        Height = 20,
                        RadiusX = 5,
                        RadiusY = 5
                    };

                    if (i >= 1 && i <= 3)
                    {
                        Canvas.SetTop(place, canvas.Height - 80);
                        Canvas.SetLeft(place, canvas.Width - 660 + (i * 22));
                        canvas.Children.Add(place);
                    }
                    else
                    {
                        Canvas.SetTop(place, canvas.Height - 60);
                        Canvas.SetLeft(place, canvas.Width - 660 + (i * 22));
                        canvas.Children.Add(place);
                    }
                }
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
