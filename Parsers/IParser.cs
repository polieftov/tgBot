using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Types;

namespace TelegramBot.Parsers
{
    public interface IParser //содержит методы для извлечения информации с сайта
    {
        public string Parse(string s, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update);
    }
}
