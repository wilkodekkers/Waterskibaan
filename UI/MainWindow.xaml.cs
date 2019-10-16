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
        private readonly CanvasRenderer _canvasRenderer;

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

            _dispatcherTimer.Tick += (source, args) => canvas.Children.Clear();
            _dispatcherTimer.Tick += DrawGame;

            _game.Initialize(_dispatcherTimer);
            _dispatcherTimer.Start();
        }

        private void DrawGame(object sender, EventArgs e)
        {
            _canvasRenderer.Render();

            lb_amountOfVisitors.Content = $"Aantal bezoekers: {_game.logger.GetAmountOfVisitors()}";
            lb_highestScore.Content = $"Hoogste behaalde score: {_game.logger.GetHighestScore()}";
            lb_amountOfVisitorsWithRedClothes.Content = $"Aantal bezoekers met rode kleding: {_game.logger.GetAmountOfAthletesWithRedClothes()}";
            sp_colors.Children.Clear();
            foreach (Color color in _game.logger.GetListWithLightestClothes())
            {
                Label label = new Label
                {
                    Content = color,
                    Background = new SolidColorBrush(color)
                };
                sp_colors.Children.Add(label);
            }
            lb_amountOfLabs.Content = $"Aantal rondes: {_game.logger.GetAmountOfLaps()}";
            sp_moves.Children.Clear();
            foreach (string move in _game.logger.GetUniqueMoves())
            {
                Label label = new Label
                {
                    Content = move
                };
                sp_moves.Children.Add(label);
            }
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