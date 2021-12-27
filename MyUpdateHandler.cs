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

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            
        }
        private bool _scheduleGroup = false;
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            if (messageText != null)
            {
                if (_scheduleGroup)
                {
                    var scheduleCmd = _executor.FindCommandByName("расписание");
                    scheduleCmd.Execute(messageText, botClient, cancellationToken, update);
                    _scheduleGroup = false;
                    return;
                }

                
                var commandName = messageText.Split()[0];//парсит текст комманды
                if (commandName == "расписание")
                    _scheduleGroup = true;
                var cmd = _executor.FindCommandByName(commandName);// находит команду по имени
                if (cmd != null)
                    cmd.Execute(messageText, botClient, cancellationToken, update);//выполняет комаду, достает данные и отправляет пользователю
                else
                    await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "Не знаю такой команды");
            }
        }
    }
}
