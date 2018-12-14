using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;
using TelegramDating.Extensions;
using TelegramDating.Model.Commands.AskActions;
using TelegramDating.Model.Commands.Slash;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model
{
    public static class BotWorker
    {
        const string FileUrl = "https://api.telegram.org/file/bot{0}/{1}";

        private static UserContext UserContext { get; set; } = Container.Current.Resolve<UserContext>();

        private static TelegramBotClient _client;
        public static IReadOnlyList<SlashCommand> AvailableSlashCommandList { get; private set; } = new SlashCommand[] {
                                                                                                        new HelpCommand(),
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
                new AskSearchSex(),
                new AskPicture(),
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

        public static User StartNewUser(long userId, string username)
        {
            if (_client is null)
                throw new NullReferenceException($"TelegramBotClient is not initialized yet. " +
                                                 $"Call {typeof(BotWorker).Name}.Get() method first.");

            User currentUser = new User(userId, username);
            BotWorker.FindSlashCommand("/start").Execute(currentUser);
            return currentUser;
        }

        public static async void SendNextProfileForLike(User currentUser)
        {
            if (_client is null)
                throw new NullReferenceException($"TelegramBotClient is not initialized yet. " +
                                                 $"Call {typeof(BotWorker).Name}.Get() method first.");

            var foundUser = BotWorker.FindSomeoneForUser(currentUser);

            if (foundUser != null)
            {
                await _client.SendPhotoAsync(
                    chatId: currentUser.UserId,
                    photo: foundUser.PictureId,
                    caption: MessageFormatter.FormatProfileMessage(foundUser),
                    parseMode: ParseMode.Html,
                    replyMarkup: CallbackKeyboardExt.CreateLikeDislikeKeyboard(foundUser));
            }
            else
            {
                await _client.SendTextMessageAsync(currentUser.UserId, 
                    "Мы никого для тебя не нашли...\n" +
                    "Просто пришли мне попозже какое-нибудь текстовое сообщение, чтобы проверить, появились ли новые пользователи!");
            }
        }

        /// <summary>
        /// Returns null if can't find anyone.
        /// </summary>
        public static User FindSomeoneForUser(User currentUser)
        {
            var checkedUserIds = currentUser.Likes.Select(like => like.CheckedUser.UserId);
            var usersForSearch = UserContext.Users
                .Where(u => u.UserId != currentUser.UserId)
                .Where(u => u.ProfileCreatingState == null)
                .Where(u => !checkedUserIds.Contains(u.UserId));

            return usersForSearch.FirstOrDefault();
        }

        public static async void RemoveKeyboard(Telegram.Bot.Types.CallbackQuery cquery)
        {
            if (_client is null)
                throw new NullReferenceException($"TelegramBotClient is not initialized yet. " +
                                                 $"Call {typeof(BotWorker).Name}.Get() method first.");

            await _client.EditMessageReplyMarkupAsync(cquery.Message.Chat.Id, cquery.Message.MessageId, replyMarkup: null);
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
            int nextIndex = GetAskActionIndex(pce) + 1;

            if (nextIndex < _profileCreatingAskActions.Count)
                return _profileCreatingAskActions.ElementAt(nextIndex);
            else
                return null;
        }

        public static AskAction FindAskAction(int index)
        {
            return _profileCreatingAskActions.ElementAt(index);
        }

        public static SlashCommand FindSlashCommand(string messageText)
        {
            return AvailableSlashCommandList.SingleOrDefault(cmd => messageText == cmd.SlashText);
        }
    }
}
