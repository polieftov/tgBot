using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class MyUpdateHandler : IUpdateHandler
    {
        private CommandsExecutor executor;
        public MyUpdateHandler(CommandsExecutor executor)
        {
            this.executor = executor;
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var messageText = update.Message.Text;
            var commandName = messageText.Split()[0];//парсит текст комманды
            var cmd = executor.FindCommandByName(commandName);// находит команду по имени
            var res = cmd.Execute(messageText, botClient, cancellationToken);//выполняет комаду, достает данные и отправляет пользователю

            await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: res.Substring(0, 200));
        }
    }
}
