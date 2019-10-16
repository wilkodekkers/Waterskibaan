using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using UI.Renderer;
using Waterskibaan;

namespace UI
{
    class DataRenderer : IRenderer
    {
        private Logger Logger { get; set; }
        private MainWindow MainWindow { get; set; }

        public DataRenderer(Logger logger, MainWindow mainWindow)
        {
            Logger = logger;
            MainWindow = mainWindow;
        }

        public void Render()
        {
            MainWindow.lb_amountOfVisitors.Content = $"Aantal bezoekers: {Logger.GetAmountOfVisitors()}";
            MainWindow.lb_highestScore.Content = $"Hoogste behaalde score: {Logger.GetHighestScore()}";
            MainWindow.lb_amountOfVisitorsWithRedClothes.Content = $"Aantal bezoekers met rode kleding: {Logger.GetAmountOfAthletesWithRedClothes()}";
            MainWindow.sp_colors.Children.Clear();
            foreach (Color color in Logger.GetListWithLightestClothes())
            {
                Label label = new Label
                {
                    Content = color,
                    Background = new SolidColorBrush(color)
                };
                MainWindow.sp_colors.Children.Add(label);
            }
            MainWindow.lb_amountOfLabs.Content = $"Aantal rondes: {Logger.GetAmountOfLaps()}";
            MainWindow.sp_moves.Children.Clear();
            foreach (IMoves move in Logger.GetUniqueMoves())
            {
                Label label = new Label
                {
                    Content = move.ToString(),
                    Background = new SolidColorBrush(Colors.LightGray)
                };
                MainWindow.sp_moves.Children.Add(label);
            }
        }
    }
}
