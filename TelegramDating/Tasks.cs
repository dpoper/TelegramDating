using Telegram.Bot;

namespace TelegramDating
{
    public static class Tasks
    {
        public static async void Welcome(this TelegramBotClient Bot, long ChatId)
        {
            await Bot.SendTextMessageAsync(
                ChatId,
                "Здарова ебать ты знакомств ищешь неудачник сколько тебе лет");

            

        }

    }
}
