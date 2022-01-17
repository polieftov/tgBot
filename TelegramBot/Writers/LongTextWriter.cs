using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Writers
{
    public class LongTextWriter : Writer
    {
        public LongTextWriter(ITelegramBotClient _botClient, Lazy<ICommandsExecutor> executor)
        {
            BotClient = _botClient;
            this.CommandsExecutor = executor;
        }

        private ReplyKeyboardMarkup GetReplyMarkups(Dictionary<string, object> markups = null) //выводит кнопки с возможными командами
        {
            var keyBoardButtons = new List<KeyboardButton[]>();
            string[] replyMarkupTextArray = markups == null ?
                CommandsExecutor.Value.GetCommands().Select(command => command.Name).ToArray() :
                markups.Keys.ToArray();

            for (var i = 0; i < replyMarkupTextArray.Length; i += 1)
            {
                keyBoardButtons.Add(new KeyboardButton[1] { replyMarkupTextArray[i] });
            }
            
            return new ReplyKeyboardMarkup(keyBoardButtons) { ResizeKeyboard = true };
        }

        public override async Task WriteAsync(string messageText, CancellationToken cancellationToken, Update update, Dictionary<string, object> settings = null)
        {
            Console.WriteLine(messageText);
            for (var i = 0; i < messageText.Length; i += 4000)
            {
                if (messageText.Length < 4000)
                {
                    await BotClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: messageText,
                        replyMarkup: GetReplyMarkups(settings));
                }
                else if (messageText.Length < i + 4000)
                {
                    await BotClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: messageText.Substring(i, messageText.Length - 4000),
                        replyMarkup: GetReplyMarkups(settings));
                }
                else
                {
                    await BotClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: messageText.Substring(i, 4000));
                }
            }
        }
    }
}
