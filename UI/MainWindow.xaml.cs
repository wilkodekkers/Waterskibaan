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
        private readonly LinkedList<Sporter> NewSporters = new LinkedList<Sporter>();
        private readonly LinkedList<Sporter> FinishedSporters = new LinkedList<Sporter>();

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
            FinishedSporters.Remove(e.Sporter);
        }

        private void OnInstructieAfgelopen(InstructieAfgelopenArgs e)
        {
            foreach (Sporter sporter in e.SportersNieuw)
            {
                NewVisitors.Remove(sporter);
                NewSporters.AddLast(sporter);
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

            DrawQueue(NewVisitors, 1);
            DrawQueue(NewSporters, 2);
            DrawQueue(FinishedSporters, 3);

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
            //Color color = sporter.KledingKleur;
            //SolidColorBrush fillBrush = new SolidColorBrush(color);
            //SolidColorBrush strokeBrush = new SolidColorBrush(Colors.Black);

            //Rectangle leftEllipse = new Rectangle
            //{
            //    Stroke = strokeBrush,
            //    Fill = fillBrush,
            //    Height = 20,
            //    Width = 20,
            //    RadiusX = 5,
            //    RadiusY = 5
            //};

            //int setLeft = 5 + ((leftOffset - 1) * 32);

            //Canvas.SetTop(leftEllipse, 5 + offset);
            //Canvas.SetLeft(leftEllipse, setLeft);

            //canvas.Children.Add(leftEllipse);
        }
    }
}
