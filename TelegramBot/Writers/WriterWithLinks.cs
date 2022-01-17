using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Writers
{
    public class WriterWithLinks : Writer
    {
        public WriterWithLinks(ITelegramBotClient _botClient)
        {
            BotClient = _botClient;
        }
        private List<InlineKeyboardButton> _links = new List<InlineKeyboardButton>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="update"></param>
        /// <param name="settings"></param> ключ - текст кнопки с ссылкой, значение - ссылка
        public override async Task WriteAsync(string text, CancellationToken cancellationToken, Update update, Dictionary<string, object>? settings = null)
        {
            List<InlineKeyboardButton[]> inlineKeyboardButtonsList = new List<InlineKeyboardButton[]>();
            if (settings != null)
            {
                
                foreach (var setting in settings)
                {
                    inlineKeyboardButtonsList.Add(new InlineKeyboardButton[] { InlineKeyboardButton.WithUrl(setting.Key, setting.Value.ToString()) });
                }
            }
            
            await BotClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: text,
                replyMarkup: new InlineKeyboardMarkup(inlineKeyboardButtonsList.ToArray())
                );
        }
    }
}