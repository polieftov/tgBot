using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Commands
{
    public class StartCommand : MyBotCommand
    {
        public StartCommand()
        {
            name = "/start";
        }


        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            return "Я вас категорически приветствую!";
        }
    }
}
