using System;
using TelegramDating.Bot;
using Castle.Windsor.Installer;

namespace TelegramDating
{
    internal class Program
    {
        public const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";

        public static void Main()
        {
            Console.Title = "Telegram Dating Bot <3";

            Container.Current.Install(FromAssembly.This());

            var me = Container.Current.Resolve<BotWorker>().Instance.GetMeAsync().Result;
            
            Console.WriteLine($"Listening: {me.FirstName}\n");

            while (true) ;
        }

    }
}
