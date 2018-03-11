using System;
using System.Threading.Tasks;
using Telegram.Bot;
using BotClient = TelegramDating.Models.Bot;

namespace TelegramDating
{
    public static class Program
    {
        public const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";

        static async Task Main(string[] args)
        {
            Database.Show();

            var Bot = await BotClient.Get();
            var me = await Bot.GetMeAsync();

            Console.ReadKey();
        }

    }
}
