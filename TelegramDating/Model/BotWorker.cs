using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Telegram.Bot;
using TelegramDating.Model.Commands.AskActions;
using TelegramDating.Model.Commands.Slash;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model
{
    public static class BotWorker
    {
        const string FileUrl = "https://api.telegram.org/file/bot{0}/{1}";

        private static TelegramBotClient _client;
        private static IReadOnlyList<SlashCommand> _slashCommandList = new SlashCommand[] {
                                                                            new StartCommand(),
                                                                            new ResetCommand(),
                                                                            new MyProfileCommand(),
                                                                      };

        private static IList<AskAction> _profileCreatingAskActions =
            new List<AskAction>
            {
                new AskName(),
                new AskSex(),
                new AskAge(),
                new AskCountry(),
                new AskCity(),
                new AskAbout(),
                new AskPicture(),
                new AskSearchSex(),
            };

        /// <summary>
        /// Create instance of bot.
        /// </summary>
        /// <param name="token">Telegram bot token. Use @BotFather to get your bot's token.</param>
        /// <returns></returns>
        public static TelegramBotClient Get(string token = Program.Token)
        {
            if (_client != null) return _client;

            _client = new TelegramBotClient(token);

            _client.StartReceiving();
            _client.OnMessage       += MessageHandler.HandleMessage;
            _client.OnCallbackQuery += MessageHandler.HandleCallbackQuery;
            
            return _client;

        }

        public static AskAction FindAskAction(ProfileCreatingEnum pce)
        {
            return _profileCreatingAskActions.FirstOrDefault(aa => aa.Id == (int)pce);
        }

        public static int GetAskActionIndex(ProfileCreatingEnum pce)
        {
            return _profileCreatingAskActions.IndexOf(pce);
        }

        public static AskAction GetNextAskAction(ProfileCreatingEnum pce)
        {
            try
            {
                return _profileCreatingAskActions.ElementAt(GetAskActionIndex(pce) + 1);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                return null;
            }
        }

        public static AskAction FindAskAction(int index)
        {
            return _profileCreatingAskActions.ElementAt(index);
        }

        public static SlashCommand FindSlashCommand(string messageText)
        {
            return _slashCommandList.SingleOrDefault(cmd => messageText == cmd.SlashText);
        }
    }
}
