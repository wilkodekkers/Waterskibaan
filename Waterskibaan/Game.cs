using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Waterskibaan
{
    class Game
    {
        private Timer _timer;
        private int _elapsed;

        private Waterskibaan _waterskibaan = new Waterskibaan();
        private WachtrijInstructie _wachtrijInstrucie = new WachtrijInstructie();
        private InstructieGroep _instructieGroep = new InstructieGroep();
        private WachtrijStarten _wachtrijStarten = new WachtrijStarten();

        public void Initialize()
        {
            SetupTimer();

            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();

            _timer.Stop();
            _timer.Dispose();
        }

        private void SetupTimer()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            _elapsed++;

            Sporter sporter = new Sporter(MoveCollection.GetWillekeurigeMoves());
            sporter.Skies = new Skies();
            sporter.Zwemvest = new Zwemvest();

            _waterskibaan.SporterStart(sporter);
            _waterskibaan.VerplaatsKabel();

            Console.WriteLine(_waterskibaan);
        }
    }
}
