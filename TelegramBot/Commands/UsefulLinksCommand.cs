using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Infrastructure;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    public class UsefulLinksCommand : MyBotCommand
    {
        public UsefulLinksCommand(Writer writer)
        {
            Writer = writer;
            Name = "Полезные ссылки";
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var props = UsefulLinksRepository.UsefulLinks.ToDictionary(keyValue => keyValue.Key, keyValue => (object)keyValue.Value);
            Writer.WriteAsync("Полезные ссылки:", cancellationToken, update, props);
            return "Полезные ссылки";
        }
    }
}
