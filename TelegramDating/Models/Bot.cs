using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramDating.Models.Commands;

namespace TelegramDating.Models
{
    public static partial class Bot
    {
        const string FileURL = "https://api.telegram.org/file/bot{0}/{1}";

        private static TelegramBotClient client;
        private static List<Command> commandList;

        
        public static IReadOnlyList<Command> Commands { get => commandList.AsReadOnly(); }
             
        /// <summary>
        /// Create instance of bot.
        /// </summary>
        /// <returns></returns>
        public static async Task<TelegramBotClient> Get(string Token)
        {
            if (client != null) return client;

            client = new TelegramBotClient(Token);

            commandList = new List<Command>() {
                new StartCommand(),
                new ResetCommand()
            };


            // await client.SetWebhookAsync();
            client.StartReceiving();
            client.OnMessage += HandleMessage;

            return client;

        }

    }
}
