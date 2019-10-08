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

        public MainWindow()
        {
            InitializeComponent();

            Game = new Game();

            DispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };

            Game.NieuweBezoeker += OnNieuweBezoeker;

            Game.Initialize(DispatcherTimer);

            DispatcherTimer.Tick += TimerEvent;
            DispatcherTimer.Start();
        }

        private void OnNieuweBezoeker(NieuweBezoekerArgs e)
        {
            NewVisitors.AddFirst(e.Sporter);
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            int count = 0;

            foreach (Sporter sporter in NewVisitors)
            {
                // Draw new visitor
                Color color = sporter.KledingKleur;
                SolidColorBrush fillBrush = new SolidColorBrush(color);
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

                Canvas.SetTop(leftEllipse, 5 + (25 * count));
                Canvas.SetLeft(leftEllipse, 5);

                canvas.Children.Add(leftEllipse);

                count++;
            }

            label.Content = Game.ToString();
        }
    }
}
