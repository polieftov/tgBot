using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class DocumentLinksCommand : MyBotCommand
    {
        public DocumentLinksCommand()
        {
            Name = "Ссылки на документы";
        }
        
        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var props = new Dictionary<string, object>();
            return "";
        }
    }
}