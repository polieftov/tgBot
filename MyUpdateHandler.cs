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
        private ICommandsExecutor executor;
        public MyUpdateHandler(ICommandsExecutor executor)
        {
            this.executor = executor;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
            
        }
        private bool scheduleGroup = false;
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            if (messageText != null)
            {
                if (scheduleGroup)
                {
                    var scheduleCmd = executor.FindCommandByName("расписание");
                    scheduleCmd.Execute(messageText, botClient, cancellationToken, update);
                    scheduleGroup = false;
                    return;
                }

                
                var commandName = messageText.Split()[0];//парсит текст комманды
                if (commandName == "расписание")
                    scheduleGroup = true;
                var cmd = executor.FindCommandByName(commandName);// находит команду по имени
                if (cmd != null)
                    cmd.Execute(messageText, botClient, cancellationToken, update);//выполняет комаду, достает данные и отправляет пользователю
                else
                    await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "Не знаю такой команды");
            }
        }
    }
}
