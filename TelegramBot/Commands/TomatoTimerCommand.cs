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
        private bool isRunning;
        private readonly TomatoTimer _tomatoTimer;
        public TomatoTimerCommand(Writer writer, TomatoTimer tomatoTimer)
        {
            Name = "Tomato";
            Writer = writer;
            _tomatoTimer = tomatoTimer;
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            if (messageText != null && messageText.Split().Length > 1)
                switch (messageText.Split()[1])
                {
                    case "Start":
                        if (isRunning)
                            return "Таймер уже запущен";
                        isRunning = true;
                        StartTimer(cancellationToken, update);
                        Writer.WriteAsync("Время работать! 25 минут", cancellationToken, update);
                        return "Время работать! 25 минут";
                    case "Stop":
                        isRunning = false;
                        return StopTimer(cancellationToken, update);
                    default:
                        Writer.WriteAsync("Неизвестная команда таймера", cancellationToken, update);
                        return "Неизвестная команда таймера";
                }
            else
            {
                Writer.WriteAsync("Допустимые команды: \n Tomato Start - Запуск таймера \n Tomato Stop - Остановка таймера", cancellationToken, update);
                return "Допустимые команды: \n Tomato Start - Запуск таймера \n Tomato Stop - Остановка таймера";
            }
        }

        private void StartTimerStateListener(Object p)
        {
            var tup = (ValueTuple<CancellationToken, CancellationToken, Update>)p;
            var ct = tup.Item1;
            var botCancellationToken = tup.Item2;
            var update = tup.Item3;
            var state = _tomatoTimer.TomatoTimerState;
            while (!ct.IsCancellationRequested)
            {
                if (_tomatoTimer.TomatoTimerState != state)
                {
                    state = _tomatoTimer.TomatoTimerState;
                    switch (state)
                    {
                        case TomatoTimerStateEnum.Work:
                            Writer.WriteAsync("Время работать! 25 минут", botCancellationToken, update);
                            break;
                        case TomatoTimerStateEnum.LongChill:
                            Writer.WriteAsync("Большой перерыв, 10 минут", botCancellationToken, update);
                            break;
                        case TomatoTimerStateEnum.ShortChill:
                            Writer.WriteAsync("Маленький перерыв, 5 минут", botCancellationToken, update);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void StartTimer(CancellationToken botCancellationToken, Update update)
        {
            var tomatoTimerState = _tomatoTimer.TomatoTimerState;
            _tomatoTimer.StartTimer();
            var ct = _tomatoTimer.GetCancellationTokenSource();
            var timerStateListenerThread = new Thread(new ParameterizedThreadStart(StartTimerStateListener));
            timerStateListenerThread.Start((ct.Token, botCancellationToken, update));
        }

        private string StopTimer(CancellationToken cancellationToken, Update update)
        {
            _tomatoTimer.StopTimer();
            Writer.WriteAsync("Таймер остановлен", cancellationToken, update);
            return "Таймер остановлен";
        }
    }
}