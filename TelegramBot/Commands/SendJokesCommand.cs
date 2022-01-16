using System;
using System.Collections.Generic;
using System.Text;
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
            Name = "/анекдот";
            Writer = writer;
        }
        
        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            Writer.WriteAsync($"Внимание анекдот! \n {JokesRepository.GetRandomJoke()}", cancellationToken, update); 
            return JokesRepository.GetRandomJoke();
        }
    }
}
