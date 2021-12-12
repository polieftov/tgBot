using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;

namespace TelegramBot.Commands
{
    public abstract class MyBotCommand// команды боту по типу /расписание, /баллы и тд.
    {

        public IParser parser;
        public string name;

        public abstract string Execute(string messageText, ITelegramBotClient botClient, CancellationToken cancellationToken);// выполнение команды, отсюда обращаемся к Parser, который достает нужную информацию, обрабатываем ее и выводим через Writer
    }
}
