using System.Threading.Tasks;

namespace TelegramBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await BotStarter.StartAsync();//запуск бота
        }
    }
}
