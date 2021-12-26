using System.Threading;
using System;
using System.Threading.Tasks;

namespace TelegramBot.Infrastructure
{
    public class TomatoTimer
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        public CancellationTokenSource getCancellationTokenSource() => cts;
        public TomatoTimerStateEnum state { get; private set; }

        public void StartTimer()
        {
            new Thread(TomatoLoop).Start(cts.Token);
        }

        private async void TomatoLoop(object p)
        {
            var ct = (CancellationToken) p;
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    state = TomatoTimerStateEnum.Work;
                    await Task.Delay(25 * 60 * 1000, ct);
                    state = TomatoTimerStateEnum.ShortChill;
                    await Task.Delay(5 * 60 * 1000, ct);
                    state = TomatoTimerStateEnum.Work;
                    await Task.Delay(10 * 60 * 1000, ct);
                    state = TomatoTimerStateEnum.LongChill;
                    await Task.Delay(1000, ct);
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("ПОток томата закрыт");
            }
        }

        public void StopTimer()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();
        }
    }
}