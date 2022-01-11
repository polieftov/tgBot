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
    public class ScheduleCommand : MyBotCommand
    {
        private bool _commandInc = true;
        private readonly IParser _parser;
        public ScheduleCommand(IParser parser, Writer writer)
        {
            Name = "расписание";
            _parser = parser;
            Writer = writer;
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            if (_commandInc)
            {
                Writer.WriteAsync("Введите номер группы", cancellationToken, update);
                _commandInc = false;
                return "Введите номер группы";
            }

            var group = messageText;

            if (String.IsNullOrWhiteSpace(group))
            {
                Writer.WriteAsync("Группа не введена", cancellationToken, update);
                return "Группа не введена";
            }

            _commandInc = true;
            var responseText = _parser.Parse(group, botClient, cancellationToken, update);
            Writer.WriteAsync(responseText, cancellationToken, update);
            return responseText;
        }
    }
}
