using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;

namespace TelegramBot.Commands
{
    public class StartCommand : MyBotCommand
    {
        public StartCommand()
        {
            name = "/start";
        }


        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            return "Я вас категорически приветствую!";
        }
    }
}
