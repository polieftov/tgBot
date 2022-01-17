using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class MyUpdateHandler : IUpdateHandler
    {
        private readonly ICommandsExecutor _executor;

        public MyUpdateHandler(ICommandsExecutor executor)
        {
            _executor = executor;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            AppDomain.CurrentDomain.UnhandledException += ProcessException;
        }

        static void ProcessException(object sender, UnhandledExceptionEventArgs args)
        {
            Console.WriteLine((args.ExceptionObject as Exception).StackTrace);
            Environment.Exit(1);
        }

        private bool _scheduleGroup = false;

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                var messageText = update.Message.Text;
                if (messageText != null)
                {
                    if (_scheduleGroup)
                    {
                        var scheduleCmd = _executor.FindCommandByName(new [] {"расписание"});
                        scheduleCmd.Execute(messageText, botClient, cancellationToken, update);
                        _scheduleGroup = false;
                        return;
                    }

                    var splittedText = messageText.Split();
                    if (splittedText[0] == "расписание")
                        _scheduleGroup = true;
                    var cmd = _executor.FindCommandByName(splittedText); // находит команду по имени
                    
                    if (cmd != null)
                        cmd.Execute(messageText, botClient, cancellationToken, update); //выполняет комаду, достает данные и отправляет пользователю
                    else
                        await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id,
                            text: "Не знаю такой команды");
                }
            }
        }
    }
}