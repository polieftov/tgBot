using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramBot.Requests;

namespace TelegramBot.Parsers
{
    class ScheduleParser : IParser
    {
        public string Parse(string group)
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
            return getScheduleRequest.Response;//пока что возвращает месиво из html, надо парсить html
        }
    }
}
