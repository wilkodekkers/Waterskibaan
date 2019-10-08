using System;
using System.Windows;
using System.Windows.Threading;
using Waterskibaan;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer DispatcherTimer;

        public MainWindow()
        {
            InitializeComponent();

            Game game = new Game();

            DispatcherTimer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(35)
            };

            game.Initialize(DispatcherTimer);

            DispatcherTimer.Tick += TimerEvent;
            DispatcherTimer.Start();
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            // Update UI
        }
    }
}
