using System.Threading;
using System;
using System.Threading.Tasks;

namespace TelegramBot.Infrastructure
{
    public class TomatoTimer
    {
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
                    await Task.Delay(25 * 60 * 1000, ct);
                    TomatoTimerState = TomatoTimerStateEnum.ShortChill;
                    await Task.Delay(5 * 60 * 1000, ct);
                    TomatoTimerState = TomatoTimerStateEnum.Work;
                    await Task.Delay(10 * 60 * 1000, ct);
                    TomatoTimerState = TomatoTimerStateEnum.LongChill;
                    await Task.Delay(1000, ct);
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