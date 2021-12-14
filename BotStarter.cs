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
using TelegramBot.Parsers;
using TelegramBot.Writers;

namespace TelegramBot
{
    class BotStarter
    {

        public static async Task StartAsync()
        {
            var botClient = new TelegramBotClient("2101396985:AAEkHghwAmWaKuKG2wYzjp9mVbhvFXBx-LQ");
            using var cts = new CancellationTokenSource();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            var updateHandler = new MyUpdateHandler(new CommandsExecutor(new MyBotCommand[2] {
                new ScheduleCommand(new ScheduleParser(), new LongTextWriter(botClient)), new StartCommand()
            }));// создать через Bind https://ulearn.me/course/cs2/Kollektsii_9187f9a6-281f-4151-a1f9-010d2ff1b54a

            botClient.StartReceiving(updateHandler);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }

    }
}
