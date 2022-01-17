using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using Newtonsoft.Json.Linq;
using TelegramBot.Infrastructure.Requests;

namespace TelegramBot.Parsers
{
    public class ScheduleParser : IParser
    {
        public string Parse(string group)
        {
            var groupId = GetGroupId(group);
            if (!String.IsNullOrEmpty(groupId))
            {
                var date = DateTime.Today.ToString("yyyyMMdd");
                var res = ScheduleParseAsync($"https://urfu.ru/api/schedule/groups/lessons/{groupId}/{date}/");
                return res.Result;
            }
            return "Неверная группа";
        }

        private string GetGroupId(string group)
        {
            var getGroupIdRequest = new GetRequest($"https://urfu.ru/api/schedule/groups/suggest/?query={group}");
            getGroupIdRequest.Run();
            var groupIdResponse = getGroupIdRequest.Response;
            if (groupIdResponse != "[]")
            {
                JArray suggestions = (JArray) JObject.Parse(groupIdResponse)["suggestions"];

                dynamic groupId = suggestions.Descendants().OfType<JObject>().FirstOrDefault(x => x["data"] != null);
                if (groupId == null)
                    return "";
                Console.WriteLine(groupId.data);
                return groupId.data.ToString();
            }
            return "";
        }

        private async Task<string> ScheduleParseAsync(string adress)
        {
            var stringBuilderSchedule = new StringBuilder();
            var config = Configuration.Default.WithDefaultLoader();
            var source = adress;
            var doc = await BrowsingContext.New(config).OpenAsync(source);
            var allTr = doc.QuerySelectorAll("tr");
            foreach (var tr in allTr)
            {
                if (tr.ClassName == "divide")
                {
                    if (tr.TextContent.Trim() != " ")
                    {
                        stringBuilderSchedule.AppendLine(tr.TextContent.Trim());
                    }
                }
                else if (tr.ClassName == "shedule-weekday-row")
                {
                    var textContDay = tr.QuerySelector("span.shedule-weekday-name");
                    var textContTime = tr.QuerySelector("td.shedule-weekday-time");
                    var textContItem = tr.QuerySelector("dl.shedule-weekday-item");
                    var textContItemParse = textContItem.TextContent.Trim();
                    textContItemParse = textContItemParse.Replace(".", "");
                    textContItemParse = textContItemParse.Replace("\n", "");
                    textContItemParse = textContItemParse.Replace("                                                                                                    ", " ");
                    textContItemParse = textContItemParse.Replace("                                                                      ", " ");
                    stringBuilderSchedule.AppendLine(textContDay == null ? " " : textContDay.TextContent.Trim());
                    stringBuilderSchedule.AppendLine(textContTime.TextContent);
                    stringBuilderSchedule.AppendLine(textContItemParse);
                }
                else if (tr.ClassName == "shedule-weekday-row shedule-weekday-first-row")
                {
                    stringBuilderSchedule.AppendLine(tr.TextContent.Trim());
                }
            }
            return stringBuilderSchedule.ToString();
        }
    }
}
