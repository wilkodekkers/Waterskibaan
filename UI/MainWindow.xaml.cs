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
        private readonly LinkedList<Sporter> VisitorQueue = new LinkedList<Sporter>();
        private readonly LinkedList<Sporter> InstructionQueue = new LinkedList<Sporter>();
        private readonly LinkedList<Sporter> StartQueue = new LinkedList<Sporter>();
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
                VisitorQueue.Remove(sporter);
                InstructionQueue.AddLast(sporter);
            }

            foreach (Sporter sporter in e.SportersKlaar)
            {
                InstructionQueue.Remove(sporter);
                StartQueue.AddLast(sporter);
            }
        }

        private void OnNieuweBezoeker(NieuweBezoekerArgs e)
        {
            VisitorQueue.AddLast(e.Sporter);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            canvas.Children.Clear();

            DrawQueue(VisitorQueue, 1);
            DrawQueue(InstructionQueue, 2);
            DrawQueue(StartQueue, 3);
            DrawQueue(ActiveSporters, 4);

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

            SolidColorBrush fillBrush = (SolidColorBrush) converter.ConvertFromString(sporter.KledingKleur);
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
    }
}
