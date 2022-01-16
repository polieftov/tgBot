namespace TelegramBot.Parsers
{
    public interface IParser //содержит методы для извлечения информации с сайта
    {
        public string Parse(string s);
    }
}
