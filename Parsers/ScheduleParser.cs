using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramBot.Requests;
using TelegramBot.Parsers;
using Telegram.Bot;
using System.Threading;
using Telegram.Bot.Types;

namespace TelegramBot.Parsers
{
    class ScheduleParser : IParser
    {
        public string Parse(string group, ITelegramBotClient botClient, CancellationToken cancellationToken, Update update)
        {
            var getGroupIdrequest = new GetRequest($"https://urfu.ru/api/schedule/groups/suggest/?query={group}");
            getGroupIdrequest.Run();
            var groupIdResponse = getGroupIdrequest.Response;
            JArray suggestions = (JArray)JObject.Parse(groupIdResponse)["suggestions"];

            dynamic groupId = suggestions.Descendants().OfType<JObject>().Where(x => x["data"] != null).FirstOrDefault();
            Console.WriteLine(groupId.data);
            
            var date = DateTime.Today.ToString("yyyyMMdd");
            var kek = $"https://urfu.ru/api/schedule/groups/lessons/{groupId.data}/{date}/";
            var getScheduleRequest = new GetRequest($"https://urfu.ru/api/schedule/groups/lessons/{groupId.data}/{date}/");
            getScheduleRequest.Run();
            HtmlParser.LolAsync($"https://urfu.ru/api/schedule/groups/lessons/{groupId.data}/{date}/", botClient, cancellationToken, update);
            
            return getScheduleRequest.Response;//пока что возвращает месиво из html, надо парсить html
            //я щас здесь насру
        }
    }
}
