using TelegramBot.Commands;

namespace TelegramBot
{
    public interface ICommandsExecutor
    {
        public MyBotCommand[] GetCommands();
        public MyBotCommand FindCommandByName(string[] splittedText);
    }
}