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

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            var commandName = messageText.Split()[0];//парсит текст комманды
            var cmd = executor.FindCommandByName(commandName);// находит команду по имени
            
            if (cmd != null)
                cmd.Execute(messageText, botClient, cancellationToken, update);//выполняет комаду, достает данные и отправляет пользователю
            else
                await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: "Не знаю такой команды");
            
        }
    }
}
