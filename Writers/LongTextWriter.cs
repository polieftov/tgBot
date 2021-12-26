﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Writers
{
    class LongTextWriter : IWriter
    {
        
        public LongTextWriter(ITelegramBotClient _botClient, Lazy<ICommandsExecutor> executor)
        {
            botClient = _botClient;
            this.commandsExecutor = executor;
        }

        private ReplyKeyboardMarkup GetReplyMarkups() //выводит кнопки с возможными командами
        {
            var keyBoardButtons = new List<KeyboardButton[]>();
            var commandNames = commandsExecutor.Value.getCommands().Select(command => command.name).ToArray();
            for (var i = 0; i < commandNames.Length; i += 4)
            {
                var segment = commandNames;//new ArraySegment<string>(commandNames, i, 4).ToArray();
                keyBoardButtons.Add(new KeyboardButton[2] { segment[0], segment[1] });
            }
            return new ReplyKeyboardMarkup(keyBoardButtons) { ResizeKeyboard = true };
        }

        public override async Task WriteAsync(string messageText, CancellationToken cancellationToken, Update update)
        {
            Console.WriteLine(messageText);
            for (var i = 0; i < messageText.Length; i += 4000)
            {
                if (messageText.Length < 4000)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: messageText
                        /*replyMarkup: GetReplyMarkups()*/);
                }
                else if (messageText.Length < i + 4000)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: messageText.Substring(i, messageText.Length - 4000)
                        /*replyMarkup: GetReplyMarkups()*/);
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: messageText.Substring(i, 4000)
                        /*replyMarkup: GetReplyMarkups()*/);
                }
            }
        }


    }
}
