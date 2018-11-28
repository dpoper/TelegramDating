using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using TelegramDating.Model.Commands.ChatActions;
using TelegramDating.Model.Commands.Slash;

namespace TelegramDating.Model
{
    public static class BotWorker
    {
        const string FileUrl = "https://api.telegram.org/file/bot{0}/{1}";

        private static TelegramBotClient _client;
        private static IEnumerable<SlashCommand> _slashCommandList;

        private static ChatAction[] _availableActions;

        /// <summary>
        /// Create instance of bot.
        /// </summary>
        /// <param name="token">Telegram bot token. Use @BotFather to get your bot's token.</param>
        /// <returns></returns>
        public static TelegramBotClient Get(string token = Program.Token)
        {
            if (_client != null) return _client;

            _client = new TelegramBotClient(token);

            _slashCommandList = new List<SlashCommand> {
                new StartCommand(),
                new ResetCommand()
            };
            
            _availableActions = new ChatAction[]
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

            _client.StartReceiving();
            _client.OnMessage += MessageHandler.HandleMessage;
            _client.OnCallbackQuery += MessageHandler.HandleCallbackQuery;

            return _client;

        }

        public static ChatAction FindAction(int actionId)
        {
            return _availableActions.SingleOrDefault(act => act.Id == actionId);

        }

        public static SlashCommand FindSlashCommand(string messageText)
        {
            return _slashCommandList.SingleOrDefault(cmd => messageText == cmd.SlashText);
        }

    }
}
