using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;
using TelegramBot.Infrastructure;
using TelegramBot.Parsers;
using TelegramBot.Writers;

namespace TelegramBot
{
    static class BotStarter
    {

        private static ICommandsExecutor GetCommandsExecutor(ITelegramBotClient botClient)
        {
            var container = new StandardKernel();
            
            container.Bind<ICommandsExecutor>().To<CommandsExecutor>();
            
            container.Bind<Writer>().To<LongTextWriter>();
            container.Bind<ITelegramBotClient>().ToConstant(botClient);

            container.Bind<TomatoTimer>().To<TomatoTimer>().WhenInjectedInto<TomatoTimerCommand>();
            container.Bind<MyBotCommand>().To<TomatoTimerCommand>();

            container.Bind<MyBotCommand>().To<StartCommand>();
            
            container.Bind<IParser>().To<ScheduleParser>().WhenInjectedInto<ScheduleCommand>();
            container.Bind<MyBotCommand>().To<ScheduleCommand>();

            container.Bind<MyBotCommand>().To<SendJokesCommand>();

            return container.Get<ICommandsExecutor>();
        }

        public static async Task StartAsync()
        {
            var botClient = new TelegramBotClient("2101396985:AAH_2hzrb0BvarC98LbV1BWQauvV9z_yWMA");
            using var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            var commandsExecutor = GetCommandsExecutor(botClient);
            var updateHandler = new MyUpdateHandler(commandsExecutor);
            // создать через Bind https://ulearn.me/course/cs2/Kollektsii_9187f9a6-281f-4151-a1f9-010d2ff1b54a*/

            botClient.StartReceiving(updateHandler);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }

    }
}
