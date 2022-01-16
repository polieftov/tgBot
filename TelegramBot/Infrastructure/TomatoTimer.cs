using System.Threading;
using System;
using System.Threading.Tasks;

namespace TelegramBot.Infrastructure
{
    public class TomatoTimer
    {
        private double _workTimeInMinutes = 0.5;
        private double _shortChillTimeInMinutes = 0.5;
        private double _longChillTimeInMinutes = 0.5;

        public TomatoTimer()
        {
        }

        public TomatoTimer(double workTimeInMinutes, double shortChillTimeInMinutes, double longChillTimeInMinutes)
        {
            _workTimeInMinutes = workTimeInMinutes;
            _shortChillTimeInMinutes = shortChillTimeInMinutes;
            _longChillTimeInMinutes = longChillTimeInMinutes;
        }

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CancellationTokenSource GetCancellationTokenSource() => _cancellationTokenSource;
        public TomatoTimerStateEnum TomatoTimerState { get; private set; }

        public void StartTimer()
        {
            new Thread(TomatoLoop).Start(_cancellationTokenSource.Token);
        }

        private async void TomatoLoop(object p)
        {
            var ct = (CancellationToken) p;
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    TomatoTimerState = TomatoTimerStateEnum.Work;
                    await Task.Delay((int)(_workTimeInMinutes * 60 * 1000), ct);
                    TomatoTimerState = TomatoTimerStateEnum.ShortChill;
                    await Task.Delay((int)(_shortChillTimeInMinutes * 60 * 1000), ct);
                    TomatoTimerState = TomatoTimerStateEnum.Work;
                    await Task.Delay((int)(_workTimeInMinutes * 60 * 1000), ct);
                    TomatoTimerState = TomatoTimerStateEnum.LongChill;
                    await Task.Delay((int)(_longChillTimeInMinutes * 60 * 1000), ct);
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Поток томата закрыт");
            }
        }

        public void StopTimer()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}