using System;
using System.Linq;
using TelegramBot.Commands;

namespace TelegramBot
{
    class CommandsExecutor : ICommandsExecutor
    {
        private readonly MyBotCommand[] _commands;
        public CommandsExecutor(MyBotCommand[] commands)
        {
            this._commands = commands;
        }

        public MyBotCommand FindCommandByName(string name) =>
            _commands.FirstOrDefault(command => string.Equals(command.Name, name, StringComparison.OrdinalIgnoreCase));

        public MyBotCommand[] GetCommands() => _commands;
    }
}
