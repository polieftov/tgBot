using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp;
using AngleSharp.Dom;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Types;
using AngleSharp.Html.Parser;
using TelegramBot.Parsers;
using TelegramBot.Requests;

namespace TelegramBot.Parsers
{
    class HtmlParser
    {
        public static async System.Threading.Tasks.Task LolAsync(string adress, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var stringBuilderSchedule = new StringBuilder();
            var config = Configuration.Default.WithDefaultLoader();
            var source = adress;
            var doc = await BrowsingContext.New(config).OpenAsync(source);
            var allTr = doc.QuerySelectorAll("tr");
            for (var i = 0; i < allTr.Length; i++)
            {
                if(allTr[i].ClassName == "divide")
                {
                    if(allTr[i].TextContent.Trim() != " ")
                    {
                        stringBuilderSchedule.AppendLine(allTr[i].TextContent.Trim());
                    }
                }
                else if (allTr[i].ClassName == "shedule-weekday-row")
                {
                    var textContDay = allTr[i].QuerySelector("span.shedule-weekday-name");
                    var textContTime = allTr[i].QuerySelector("td.shedule-weekday-time");
                    var textContItem = allTr[i].QuerySelector("dl.shedule-weekday-item");
                    var textContItemParse = textContItem.TextContent.Trim();
                    textContItemParse = textContItemParse.Replace(".", "");
                    textContItemParse = textContItemParse.Replace("\n", "");
                    textContItemParse = textContItemParse.Replace("                                                                                                    ", " ");
                    textContItemParse = textContItemParse.Replace("                                                                      ", " ");
                    stringBuilderSchedule.AppendLine(textContDay == null ? " " : textContDay.TextContent.Trim());
                    stringBuilderSchedule.AppendLine(textContTime.TextContent);
                    stringBuilderSchedule.AppendLine(textContItemParse);
                }
                else if (allTr[i].ClassName == "shedule-weekday-row shedule-weekday-first-row")
                {
                    stringBuilderSchedule.AppendLine(allTr[i].TextContent.Trim());
                }
            }
            Console.WriteLine(stringBuilderSchedule.ToString());
            await botClient.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: stringBuilderSchedule.ToString());
        } 
    }
}
