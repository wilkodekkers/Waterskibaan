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
        private readonly LinkedList<Sporter> NewVisitors = new LinkedList<Sporter>();
        private readonly List<Sporter> NewSporters = new List<Sporter>();
        private readonly LinkedList<Sporter> FinishedSporters = new LinkedList<Sporter>();
        private readonly LinkedList<Sporter> ActiveSporters = new LinkedList<Sporter>();

        public MainWindow()
        {
            InitializeComponent();

            Game = new Game();

            DispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };

            Game.NieuweBezoeker += OnNieuweBezoeker;
            Game.InstructieAfgelopen += OnInstructieAfgelopen;
            Game.LijnenVerplaats += OnLijnenVerplaats;

            Game.Initialize(DispatcherTimer);

            DispatcherTimer.Tick += TimerEvent;
            DispatcherTimer.Start();
        }

        private void OnLijnenVerplaats(LijnenVerplaatsArgs e)
        {
            StartQueue.Remove(e.Sporter);
            ActiveSporters.AddFirst(e.Sporter);
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
            NewVisitors.AddLast(e.Sporter);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            canvas.Children.Clear();

            DrawInstructionQueue();
            DrawQueue(NewVisitors, 1);
            DrawQueue(FinishedSporters, 2);
            DrawQueue(ActiveSporters, 3);

            label.Content = Game.ToString();
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
    }
}
