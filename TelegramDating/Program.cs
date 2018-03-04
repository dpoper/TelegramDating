using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDating
{
    class Program
    {
        const string Token = "540041724:AAG1Q-SsvKWqk4qzyH-0X2OtVDjsOJaZ9UE";
        public static TelegramBotClient Bot = new TelegramBotClient(Token);

        static void Main(string[] args)
        {
            // Database.AddTest();

            Database.Test();

            Bot.OnMessage += BotOnMessageReceived;

            
            Console.ReadKey();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            

            Console.WriteLine(message.From.Username + ": " + message.Text);

            if (message.Text == "/start")
            {
                bool UserIsNew = Database.ContainsUser(message.Chat.Id); // Пока что. А так оно должно спрашивать у БД, есть ли там такой юзер.

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
