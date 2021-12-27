using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    public abstract class MyBotCommand// команды боту по типу /расписание, /баллы и тд.
    {
        public string Name;
        protected Writer Writer;

        public abstract string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update);// выполнение команды, отсюда обращаемся к Parser, который достает нужную информацию, обрабатываем ее и выводим через Writer
    }
}
