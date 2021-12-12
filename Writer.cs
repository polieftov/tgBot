using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    interface IWriter
    {
        public void Write(string text);// вывод сообщения пользователю в виде текста, таблицы, другие стили и тд
                                       // Echo received message text
        //Message sentMessage = await botClient.SendTextMessageAsync(
        //    chatId: chatId,
        //    text: "You said:\n" + messageText,
        //    cancellationToken: cancellationToken
        //    );
    }
}
