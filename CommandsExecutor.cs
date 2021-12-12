using System;
using System.Collections.Generic;
using System.Text;
using TelegramBot.Commands;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class CommandsExecutor
    {
        private readonly MyBotCommand[] commands;
        public CommandsExecutor(MyBotCommand[] commands)
        {
            this.commands = commands;
        }

        public ReplyKeyboardMarkup GetReplyMarkups() //выводит кнопки с возможными командами
        {
            var keyBoardButtons = new List<KeyboardButton[]>();
            var commandNames = getCommandNames();
            for (var i = 0; i < commandNames.Length; i += 4)
            {
                var segment = new ArraySegment<string>(commandNames, i, 4).ToArray();
                keyBoardButtons.Add(new KeyboardButton[4] { segment[0], segment[1], segment[2], segment[3] });
            }
            return new ReplyKeyboardMarkup(keyBoardButtons){ ResizeKeyboard = true };
        }

        public string[] getCommandNames() => commands.Select(command => command.name).ToArray();

        public MyBotCommand FindCommandByName(string name) =>
            commands.FirstOrDefault(command => string.Equals(command.name, name, StringComparison.OrdinalIgnoreCase));
        
    }
}
