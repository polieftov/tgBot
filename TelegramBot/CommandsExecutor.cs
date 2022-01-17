using System;
using System.Linq;
using TelegramBot.Commands;

namespace TelegramBot
{
    public class CommandsExecutor : ICommandsExecutor
    {
        private readonly MyBotCommand[] _commands;
        public CommandsExecutor(MyBotCommand[] commands)
        {
            _commands = commands;
        }

        public MyBotCommand FindCommandByName(string[] splittedText)
        {
            if (splittedText.Length < 0)
                return null;
            var commandName = splittedText[0];
            var cmd = _commands.FirstOrDefault(command => string.Equals(command.Name, commandName, StringComparison.InvariantCultureIgnoreCase));

            for (int i = 1; i < splittedText.Length && cmd == null; i++)
            {
                commandName += " " + splittedText[i];
                cmd = _commands.FirstOrDefault(command => string.Equals(command.Name, commandName, StringComparison.InvariantCultureIgnoreCase));
            }
            return cmd;
        }

        public MyBotCommand[] GetCommands() => _commands;
    }
}
