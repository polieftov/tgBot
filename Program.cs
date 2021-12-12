using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class Program
    {
        private static string token { get => "2101396985:AAEkHghwAmWaKuKG2wYzjp9mVbhvFXBx-LQ"; }
        static async Task Main(string[] args)
        {
            await BotStarter.StartAsync();//запуск бота
        }

    }

}
