using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Writers
{
    public abstract class Writer
    {
        protected ITelegramBotClient BotClient;
        protected Lazy<ICommandsExecutor> CommandsExecutor;

        public abstract Task WriteAsync(string text, CancellationToken cancellationToken, Update update, Dictionary<string, object>? settings = null);
    }
}
