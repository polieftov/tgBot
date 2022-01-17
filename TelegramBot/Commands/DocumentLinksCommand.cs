using System.Linq;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Infrastructure;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    public class DocumentLinksCommand : MyBotCommand
    {
        public DocumentLinksCommand(Writer writer)
        {
            Writer = writer;
            Name = "Ссылки на документы";
        }
        
        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var props = DocumentsRepository.DocumentsWithUrl.ToDictionary(keyValue => keyValue.Key, keyValue => (object) keyValue.Value);
            Writer.WriteAsync("Ссылки на документы:", cancellationToken, update, props);
            return "Ссылки на документы:";
        }
    }
}