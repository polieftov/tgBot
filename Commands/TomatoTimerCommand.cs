using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Infrastructure;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    public class TomatoTimerCommand : MyBotCommand
    {

        private TomatoTimer _tomatoTimer;
        public TomatoTimerCommand(IWriter _writer, TomatoTimer tomatoTimer)
        {
            name = "Tomato";
            writer = _writer;
            _tomatoTimer = tomatoTimer;
        }
        
        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            if (messageText.Split().Length > 1)
                switch (messageText.Split()[1])
                {
                    case "Start":
                        StartTimer(cancellationToken, update);
                        writer.WriteAsync("Время работать!, 25 минут", cancellationToken, update);
                        break;
                    case "Stop":
                        StopTimer(cancellationToken, update);
                        break;
                    default:
                        writer.WriteAsync("Неизвестная команда таймера", cancellationToken, update);
                        break;
                }
            else
            {
                writer.WriteAsync("Допустимые команды: \n Tomato Start - Запуск таймера \n Tomato Stop - Остановка таймера", cancellationToken, update);
            }

            return "1";
        }

        private void StartTimerStateListener(Object p)
        {
            var tup = (ValueTuple<CancellationToken, CancellationToken, Update>) p;
            var ct = tup.Item1;
            var botCancellationToken = tup.Item2;
            var update = tup.Item3;
            var state = _tomatoTimer.state;
            while (!ct.IsCancellationRequested)
            {
                if (_tomatoTimer.state != state)
                {
                    state = _tomatoTimer.state;
                    switch (state)
                    {
                        case TomatoTimerStateEnum.Work:
                            writer.WriteAsync("Время работать!, 25 минут", botCancellationToken, update);
                            break;
                        case TomatoTimerStateEnum.LongChill:
                            writer.WriteAsync("Большой перерыв, 10 минут", botCancellationToken, update);
                            break;
                        case TomatoTimerStateEnum.ShortChill:
                            writer.WriteAsync("Маленький перерыв, 5 минут", botCancellationToken, update);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
            
        private void StartTimer(CancellationToken botCancellationToken, Update update)
        {
            var tomatoTimerState = _tomatoTimer.state;
            _tomatoTimer.StartTimer();
            var ct = _tomatoTimer.getCancellationTokenSource();
            var timerStateListenerThread = new Thread(new ParameterizedThreadStart(StartTimerStateListener));
            timerStateListenerThread.Start((ct.Token, botCancellationToken, update));
            
            // StartTimerStateListener(new ValueTuple<CancellationTokenSource, CancellationToken, Update>(ct, botCancellationToken, update));

        }

        private void StopTimer(CancellationToken cancellationToken, Update update)
        {
            _tomatoTimer.StopTimer();
            writer.WriteAsync("Таймер остановлен", cancellationToken, update);
        }
    }
}