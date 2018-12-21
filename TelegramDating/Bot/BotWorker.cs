﻿using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;
using TelegramDating.Extensions;
using TelegramDating.Bot.Commands.AskActions;
using TelegramDating.Bot.Commands.Slash;
using TelegramDating.Enums;
using TelegramDating.Model;

namespace TelegramDating.Bot
{
    public partial class BotWorker
    {
        public UserContext UserContext { get; set; }
        public IReadOnlyList<SlashCommand> AvailableSlashCommandList { get; private set; } = new SlashCommand[] {
                                                                                                        new HelpCommand(),
                                                                                                        new StartCommand(),
                                                                                                        new ResetCommand(),
                                                                                                        new MyProfileCommand(),
                                                                                                  };

        public IList<AskAction> ProfileCreatingAskActions { get; private set; } =
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

        private static TelegramBotClient _client { get; set; }
        
        public TelegramBotClient Instance => _client;

        public BotWorker(UserContext UserContext)
        {
            this.UserContext = UserContext;
            BotWorker.Create();
        }

        /// <summary>
        /// Create instance of bot.
        /// </summary>
        /// <param name="token">Telegram bot token. Use @BotFather to get your bot's token.</param>
        /// <returns></returns>
        public static TelegramBotClient Create(string token = Program.Token)
        {
            if (_client != null) return _client;

            _client = new TelegramBotClient(token);

            return _client;
        }

        public User StartNewUser(long userId, string username)
        {
            User currentUser = new User(userId, username);
            this.FindSlashCommand("/start").Execute(currentUser);
            return currentUser;
        }

        public async void SendNextProfileForLike(User currentUser)
        {
            var foundUser = this.FindSomeoneForUser(currentUser);

            if (foundUser != null)
            {
                await this.Instance.SendPhotoAsync(
                    chatId: currentUser.UserId,
                    photo: foundUser.PictureId,
                    caption: MessageFormatter.FormatProfileMessage(foundUser),
                    parseMode: ParseMode.Html,
                    replyMarkup: CallbackKeyboardExt.CreateLikeDislikeKeyboard(foundUser));
            }
            else
            {
                await this.Instance.SendTextMessageAsync(currentUser.UserId, 
                    "Мы никого для тебя не нашли...\n" +
                    "Просто пришли мне попозже какое-нибудь текстовое сообщение, чтобы проверить, появились ли новые пользователи!");
            }
        }

        /// <summary>
        /// Returns null if can't find anyone.
        /// </summary>
        public User FindSomeoneForUser(User currentUser)
        {
            IEnumerable<long> likedUserIds = currentUser.Likes.Select(like => like.CheckedUser.UserId);
            IEnumerable<long> gotLikesUserIds = currentUser.GotLikes.Select(like => like.User.UserId);
            var usersForSearch = this.UserContext.Users
                .Where(u => u.UserId != currentUser.UserId)
                .Where(u => u.ProfileCreatingState == null)
                .Where(u => !likedUserIds.Contains(u.UserId))
                .Where(u => !gotLikesUserIds.Contains(u.UserId));

            return usersForSearch.FirstOrDefault();
        }

        public async void RemoveKeyboard(Telegram.Bot.Types.CallbackQuery cquery)
        {
            await this.Instance.EditMessageReplyMarkupAsync(cquery.Message.Chat.Id, cquery.Message.MessageId, replyMarkup: null);
        }

        public AskAction FindAskAction(ProfileCreatingEnum pce)
        {
            return this.ProfileCreatingAskActions.FirstOrDefault(aa => aa.Id == (int)pce);
        }

        public int GetAskActionIndex(ProfileCreatingEnum pce)
        {
            return this.ProfileCreatingAskActions.IndexOf(pce);
        }

        public AskAction GetNextAskAction(ProfileCreatingEnum pce)
        {
            int nextIndex = this.GetAskActionIndex(pce) + 1;

            if (nextIndex < this.ProfileCreatingAskActions.Count)
                return this.ProfileCreatingAskActions.ElementAt(nextIndex);
            else
                return null;
        }

        public AskAction FindAskAction(int index)
        {
            return this.ProfileCreatingAskActions.ElementAt(index);
        }

        public SlashCommand FindSlashCommand(string messageText)
        {
            return this.AvailableSlashCommandList.SingleOrDefault(cmd => messageText == cmd.SlashText);
        }

        public async void SendNoUsernameSetMessage(long userId)
        {
            await this.Instance.SendTextMessageAsync(userId,
                    "Упс! У тебя не проставлен юзернейм. Сделай так, чтобы к тебе можно было обращаться с собачкой. \n" +
                    $"Как ко мне: @{this.Instance.GetMeAsync().Result.Username}");
        }
    }
}