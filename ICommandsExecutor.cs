using System.Collections.Generic;
using TelegramBot.Commands;

namespace TelegramBot
{
    public interface ICommandsExecutor
    {
        public MyBotCommand[] getCommands();
        public MyBotCommand FindCommandByName(string name);
    }
}