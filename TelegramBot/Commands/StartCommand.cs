using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class StartCommand : MyBotCommand
    {
        public StartCommand()
        {
            Name = "/start";
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            Writer.WriteAsync("Я вас категорически приветствую!", cancellationToken, update);
            return "Я вас категорически приветствую!";
        }
    }
}
