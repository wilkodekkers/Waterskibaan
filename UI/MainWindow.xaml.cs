using System;
using System.Windows;
using System.Windows.Threading;
using UI.Renderer;
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
        private readonly CanvasRenderer _canvasRenderer;
        private readonly DataRenderer _dataRenderer;

        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;
            InitializeComponent();

            _game = new Game();

            _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            _canvasRenderer = new CanvasRenderer(canvas, _game);
            _dataRenderer = new DataRenderer(_game.logger, this);

            _dispatcherTimer.Tick += (source, args) => canvas.Children.Clear();
            _dispatcherTimer.Tick += DrawGame;

            _game.Initialize(_dispatcherTimer);
            _dispatcherTimer.Start();
        }

        private void DrawGame(object sender, EventArgs e)
        {
            _canvasRenderer.Render();
            _dataRenderer.Render();
        }

        private void Bt_start_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Start();
        }

        private void Bt_stop_Click(object sender, RoutedEventArgs e)
        {
            _dispatcherTimer.Stop();
        }
    }
}