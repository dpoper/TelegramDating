using System;
using System.Threading.Tasks;
using TelegramDating.Database;
using TelegramDating.Model;

namespace TelegramDating
{
    public static class Program
    {
        public const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";

        static async Task Main()
        {
            var DB = UserRepository.Initialize();

            Console.Title = "Telegram Dating Bot <3";
            Console.SetWindowSize(80, 20);

            var Bot = BotWorker.Get(Token);

            Console.WriteLine("Listening...");

            Console.ReadKey();
            DB.Dispose();
        }

    }
}
