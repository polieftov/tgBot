using System;
using System.Collections.Generic;

namespace TelegramBot.Infrastructure
{
    public static class DocumentsRepository
    {
        public static readonly Dictionary<string, string> DocumentsWithUrl = new Dictionary<string, string>()
        {
            {"Все документы", "https://urfu.ru/ru/students/documents/"}
        };

    }
}