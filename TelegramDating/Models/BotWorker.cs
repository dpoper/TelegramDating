using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramDating.Models.Commands;

namespace TelegramDating.Models
{
    public static partial class BotWorker
    {
        const string FileURL = "https://api.telegram.org/file/bot{0}/{1}";

        private static TelegramBotClient client;
        private static List<Command> commandList;


        public static IReadOnlyList<Command> Commands { get => commandList.AsReadOnly(); }

        /// <summary>
        /// Create instance of bot.
        /// </summary>
        /// <param name="Token">Telegram bot token. Use @BotFather to get your bot's token.</param>
        /// <returns></returns>
        public static async Task<TelegramBotClient> Get(string Token)
        {
            if (client != null) return client;

            client = new TelegramBotClient(Token);

            commandList = new List<Command> {
                new StartCommand(),
                new ResetCommand()
            };

            // await client.SetWebhookAsync();
            client.StartReceiving();
            client.OnMessage += HandleMessage;
            client.OnCallbackQuery += HandleCallbackQuery;

            return client;

        }

    }
}
