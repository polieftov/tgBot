using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using TelegramBot.Parsers;

namespace TelegramBot.Commands
{
    class ScheduleCommand : MyBotCommand
    {
        public ScheduleCommand(string _name, ScheduleParser _parser)
        {
            name = _name;
            parser = _parser;
        }

        public override string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            var group = messageText.Split()[1];
           
            
            return parser.Parse(group);     
            
        }
    }
}
