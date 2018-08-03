using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramDating.Model.Commands;
using TelegramDating.Model.Commands.Bot;
using TelegramDating.Model.Commands.Slash;

namespace TelegramDating.Model
{
    public static partial class BotWorker
    {
        const string FileURL = "https://api.telegram.org/file/bot{0}/{1}";

        private static TelegramBotClient client;
        private static List<ISlashCommand> slashCommandList;

        public static IReadOnlyList<ISlashCommand> SlashCommands => slashCommandList.AsReadOnly();
        private static IChatAction[] AvailableActions;

        /// <summary>
        /// Create instance of bot.
        /// </summary>
        /// <param name="Token">Telegram bot token. Use @BotFather to get your bot's token.</param>
        /// <returns></returns>
        public static TelegramBotClient Get(string Token = Program.Token)
        {
            if (client != null) return client;

            client = new TelegramBotClient(Token);

            slashCommandList = new List<ISlashCommand> {
                new StartCommand(),
                new ResetCommand()
            };
            
            AvailableActions = new IChatAction[]
            {
                new ActionHello(),
                new ActionSex(),
                new ActionName(),
                new ActionAge(),
                new ActionCountry(),
                new ActionCity(),
                new ActionPicture(),
                new ActionSearchSex(),
                new ActionSearchShow()
            };

            client.StartReceiving();
            client.OnMessage += MessageHandler.HandleMessage;
            client.OnCallbackQuery += MessageHandler.HandleCallbackQuery;

            return client;

        }

        public static IChatAction FindAction(int actionId)
        {
            var action = AvailableActions.Single(act => act.Id == actionId);
            return action;

        }

        public static ISlashCommand FindSlashCommand(string messageText)
        {
            return SlashCommands.SingleOrDefault(cmd => messageText == (cmd as ISlashCommand).SlashText);
        }

    }
}
