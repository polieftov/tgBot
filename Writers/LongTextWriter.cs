using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Writers
{
    class LongTextWriter : IWriter
    {
        public LongTextWriter(ITelegramBotClient _botClient)
        {
            botClient = _botClient;
        }

        public override async Task WriteAsync(string messageText, CancellationToken cancellationToken, Update update)
        {
            Console.WriteLine(messageText);
            for (var i = 0; i < messageText.Length; i += 4000)
            {
                if (messageText.Length < 4000)
                {
                    await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: messageText);
                }
                else if (messageText.Length < i + 4000)
                {
                    await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: messageText.Substring(i, messageText.Length - 4000));
                }
                else
                {
                    await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: messageText.Substring(i, 4000));
                }
            }
        }


    }
}
