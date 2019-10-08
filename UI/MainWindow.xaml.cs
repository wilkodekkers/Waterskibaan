using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.Generic;
using Waterskibaan;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

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

        private readonly Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.BottomRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

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
            NewVisitors.AddLast(e.Sporter);

            notifier.ShowSuccess("New Visitor joined the queue");
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            DrawNewVisitorQueue();

            label.Content = Game.ToString();
        }

        private void DrawNewVisitorQueue()
        {
            Line fence = new Line
            {
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.Black),
                X1 = 32,
                X2 = 32,
                Y1 = 0,
                Y2 = canvas.Height
            };

            canvas.Children.Add(fence);

            int count = 0;

            foreach (Sporter sporter in NewVisitors)
            {
                int offset = 25 * count;

                DrawNewVisitor(sporter, offset);

                count++;
            }
        }

        private void DrawNewVisitor(Sporter sporter, int offset)
        {
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

            Canvas.SetTop(leftEllipse, 5 + offset);
            Canvas.SetLeft(leftEllipse, 5);

            canvas.Children.Add(leftEllipse);
        }
    }
}
