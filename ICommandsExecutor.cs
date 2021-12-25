using TelegramBot.Commands;

namespace TelegramBot
{
    internal interface ICommandsExecutor
    {
        public MyBotCommand FindCommandByName(string name);
    }
}