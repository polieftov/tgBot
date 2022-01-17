using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Infrastructure;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    public class UsefullLinksCommand : MyBotCommand
    {
        public UsefullLinksCommand(Writer writer)
        {
            Writer = writer;
            Name = "Полезные ссылки";
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var props = UsefullLinksRepository.UsefullLinks.ToDictionary(keyValue => keyValue.Key, keyValue => (object)keyValue.Value);
            Writer.WriteAsync("Полезные ссылки:", cancellationToken, update, props);
            return "Полезные ссылки";
        }
    }
}
