using System;
using System.Collections.Generic;
using System.Text;
using TelegramBot.Commands;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class CommandsExecutor : ICommandsExecutor
    {
        private readonly MyBotCommand[] commands;
        public CommandsExecutor(MyBotCommand[] commands)
        {
            this.commands = commands;
        }

        public MyBotCommand FindCommandByName(string name) =>
            commands.FirstOrDefault(command => string.Equals(command.name, name, StringComparison.OrdinalIgnoreCase));

        public MyBotCommand[] getCommands() => commands;
    }
}
