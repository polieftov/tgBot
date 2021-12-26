using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Writers
{
    public abstract class IWriter
    {
        protected ITelegramBotClient botClient;
        protected Lazy<ICommandsExecutor> commandsExecutor;

        public abstract Task WriteAsync(string text, CancellationToken cancellationToken, Update update);
    }
}
