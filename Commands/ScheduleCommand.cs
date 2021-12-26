using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using TelegramBot.Parsers;
using Telegram.Bot.Types;
using TelegramBot.Writers;

namespace TelegramBot.Commands
{
    class ScheduleCommand : MyBotCommand
    {
        private bool commandInc = true;
        private IParser parser;
        public ScheduleCommand(IParser _parser, IWriter _writer)
        {
            name = "расписание";
            parser = _parser;
            writer = _writer;
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var group = "";
            if (commandInc)
            {
                writer.WriteAsync("Введите номер группы", cancellationToken, update);
                commandInc = false;
                return "";
            }

            group = messageText;
            
            if (group == null)
                return "";
            commandInc = true;
            var responseText = parser.Parse(group, botClient, cancellationToken, update);
            writer.WriteAsync(responseText, cancellationToken, update);
            return responseText;
        }
    }
}
