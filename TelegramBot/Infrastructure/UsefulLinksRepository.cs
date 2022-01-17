using System.Collections.Generic;

namespace TelegramBot.Infrastructure
{
    public static class UsefulLinksRepository
    {
        public static readonly Dictionary<string, string> UsefulLinks = new Dictionary<string, string>()
        {
            { "Главная страница", "https://urfu.ru/ru/" },
            { "Личный кабинет студента", "https://istudent.urfu.ru/" },
            { "Прокомпетенции", "https://xn--e1aajagmjdbheh6azd.xn--p1ai/" },
            { "Exam1", "https://exam1.urfu.ru/" },
            { "Modeus", "https://urfu.modeus.org/" },
            { "ГИПЕРМЕТОД", "https://learn.urfu.ru/" },
            { "TeamProject", "https://teamproject.urfu.ru/#/" }
        };
    }
}
