using System;
using System.Threading.Tasks;
using TelegramDating.Model;

namespace TelegramDating
{
    public static class Program
    {
        public const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";

        static async Task Main()
        {
            Console.Title = "Telegram Dating Bot <3";
            Console.SetWindowSize(80, 20);

            var Bot = await BotWorker.Get(Token);
            Console.WriteLine("Listening...");

            Console.ReadKey();
        }

    }
}
