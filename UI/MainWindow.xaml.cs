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
    public partial class MainWindow
    {
        private readonly DispatcherTimer _dispatcherTimer;
        private readonly Game _game;
        private readonly List<Sporter> _newVisitors = new List<Sporter>();
        private readonly List<Sporter> _newAthletes = new List<Sporter>();
        private readonly List<Sporter> _finishedAthletes = new List<Sporter>();
        private LinkedList<Lijn> _lines = new LinkedList<Lijn>();
        private readonly CanvasRenderer _canvasRenderer;

        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();

            _game = new Game();

            _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };

            _canvasRenderer = new CanvasRenderer(canvas, _game);

            _dispatcherTimer.Tick += (source, args) => canvas.Children.Clear();
            _dispatcherTimer.Tick += DrawGame;

            _game.Initialize(_dispatcherTimer);
            _dispatcherTimer.Start();
        }

        private void DrawGame(object sender, EventArgs e)
        {
            _canvasRenderer.Render();
            
            label.Content = _game.ToString();
        }

        private void bt_start_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Start();
        }

        private void bt_stop_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Stop();
        }
    }
}