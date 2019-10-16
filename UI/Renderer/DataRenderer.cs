using System.Windows.Controls;
using System.Windows.Media;
using Waterskibaan;

namespace UI.Renderer
{
    class DataRenderer : IRenderer
    {
        private Logger _logger;
        private MainWindow _mainWindow;

        public DataRenderer(Logger logger, MainWindow mainWindow)
        {
            _logger = logger;
            _mainWindow = mainWindow;
        }

        private Label CreateColorLabel(Color color)
        {
            return new Label()
            {
                Content = color,
                Background = new SolidColorBrush(color)
            };
        }

        private Label CreateMoveLabel(IMoves move)
        {
            return new Label()
            {
                Content = move.ToString(),
                Background = new SolidColorBrush(Colors.LightGray)
            };
        }

        public void Render()
        {
            _mainWindow.sp_colors.Children.Clear();
            _mainWindow.sp_moves.Children.Clear();

            _mainWindow.lb_amountOfVisitors.Content = $"Aantal bezoekers: {_logger.GetAmountOfVisitors()}";
            _mainWindow.lb_highestScore.Content = $"Hoogste behaalde score: {_logger.GetHighestScore()}";
            _mainWindow.lb_amountOfVisitorsWithRedClothes.Content = $"Aantal bezoekers met rode kleding: {_logger.GetAmountOfAthletesWithRedClothes()}";
            _logger
                .GetListWithLightestClothes()
                .ForEach(color => _mainWindow.sp_colors.Children.Add(CreateColorLabel(color)));
            _mainWindow.lb_amountOfLabs.Content = $"Aantal rondes: {_logger.GetAmountOfLaps()}";
            _logger
                .GetUniqueMoves()
                .ForEach(move => _mainWindow.sp_moves.Children.Add(CreateMoveLabel(move)));
        }
    }
}
