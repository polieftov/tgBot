using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using TelegramBot.Requests;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using AngleSharp;


namespace TelegramBot.Parsers
{
    class ScheduleParser : IParser
    {
        public string Parse(string group, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var groupId = GetGroupId(group);
            if (!String.IsNullOrEmpty(groupId))
            {
                var date = DateTime.Today.ToString("yyyyMMdd");

                var res = ScheduleParseAsync($"https://urfu.ru/api/schedule/groups/lessons/{groupId}/{date}/", botClient, cancellationToken, update);

                return res.Result;
            }
            else
                return "Неверная группа";
        }

        private string GetGroupId(string group)
        {
            var getGroupIdrequest = new GetRequest($"https://urfu.ru/api/schedule/groups/suggest/?query={group}");
            getGroupIdrequest.Run();
            var groupIdResponse = getGroupIdrequest.Response;
            if (groupIdResponse != "[]")
            {
                JArray suggestions = (JArray) JObject.Parse(groupIdResponse)["suggestions"];

                dynamic groupId = suggestions.Descendants().OfType<JObject>().Where(x => x["data"] != null)
                    .FirstOrDefault();
                Console.WriteLine(groupId.data);

                return groupId.data.ToString();
            }
            else
                return "";

        }

        private async Task<string> ScheduleParseAsync(string adress, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var stringBuilderSchedule = new StringBuilder();
            var config = Configuration.Default.WithDefaultLoader();
            var source = adress;
            var doc = await BrowsingContext.New(config).OpenAsync(source);
            var allTr = doc.QuerySelectorAll("tr");
            for (var i = 0; i < allTr.Length; i++)
            {
                if (allTr[i].ClassName == "divide")
                {
                    if (allTr[i].TextContent.Trim() != " ")
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

            return stringBuilderSchedule.ToString();
        }
    }
}
