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

            // Тут добавим сразу пустого юзера в таблицу, дальше тупо меняем ему State

            var a = new User()
            {
                Name = "Anton",
                Age = 19,
                // ...

                State = (int) State.Create.Name
            };
            
            // Типа того

        }

    }
}
