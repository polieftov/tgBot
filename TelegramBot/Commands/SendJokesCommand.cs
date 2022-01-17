using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Infrastructure;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    public class SendJokesCommand : MyBotCommand
    {
        public SendJokesCommand(Writer writer)
        {
            Name = "анекдот";
            Writer = writer;
        }
        
        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var randomJoke = JokesRepository.GetRandomJoke();
            Writer.WriteAsync($"Внимание, анекдот! \n {randomJoke}", cancellationToken, update); 
            return randomJoke;
        }
    }
}
