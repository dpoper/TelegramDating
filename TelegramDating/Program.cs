using System;
using Telegram.Bot;
using TelegramDating.Database;
using TelegramDating.Model;

namespace TelegramDating
{
    internal class Program
    {
        public const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";

        public static TelegramBotClient Bot { get; private set; }

        public static void Main()
        {
            Console.Title = "Telegram Dating Bot <3";

            Container.Current.Install(new Installer());

            Bot = BotWorker.Get(Token);

            var me = Bot.GetMeAsync().Result;

            Console.WriteLine($"Listening: {me.FirstName}\n");

            while (true) ;
        }

    }
}
