using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    interface IParser //содержит методы для извлечения информации с сайта
    {
        public string Parse(string s);
    }
}
