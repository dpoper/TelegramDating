using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramDating
{
    class Program
    {
        const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";
        public static TelegramBotClient Bot = new TelegramBotClient(Token);

        static void Main(string[] args)
        {
            Database.Show();

            // Database.AddUser(new User(1, "1", 24, 0, 151));
            // Bot.OnMessage += BotOnMessageReceived;


            Console.ReadKey();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            

            Console.WriteLine(message.From.Username + ": " + message.Text);

            if (message.Text == "/start")
            {
                bool UserIsNew = Database.ContainsUser(message.From.Username);

                if (UserIsNew)
                    Bot.Welcome(message.Chat.Id);
                else
                { 
                    // сукин сын стартует бота, когда в базе уже есть этот сукин сын.
                    // Bot.NotWelcome(), типа "здарова, помним тебя, пидрила. добро пожаловать, снова ".
                }

            }

        }


    }
}
