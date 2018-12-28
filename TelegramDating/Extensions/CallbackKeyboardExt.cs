using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Database;
using TelegramDating.Model;
using TelegramDating.Enums;
using System;

namespace TelegramDating.Extensions
{
    public static class CallbackKeyboardExt
    {
        public static class EmojiConsts
        {
            public const string Heart = "❤️";
            public const string BrokenHeart = "💔";
        }

        public static readonly InlineKeyboardMarkup Sex = new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Мальчик", ((int)SearchOptions.Sex.Male).ToString()),
            InlineKeyboardButton.WithCallbackData("Девочка", ((int)SearchOptions.Sex.Female).ToString())
        });

        public static readonly InlineKeyboardMarkup SearchSex = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Мальчика", ((int) SearchOptions.Sex.Male).ToString()),
                InlineKeyboardButton.WithCallbackData("Девочку", ((int) SearchOptions.Sex.Female).ToString()),
            },
            new[] { InlineKeyboardButton.WithCallbackData("Без разницы", ((int) SearchOptions.Sex.Any).ToString()) }
        });



        public static InlineKeyboardMarkup CreateLikeDislikeKeyboard(User profileUser, bool isForResponse = false)
        {
            var reqOrResp = isForResponse ? "resp" : "req";

            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(EmojiConsts.Heart, $"{reqOrResp} {profileUser.UserId.ToString()} true"),
                    InlineKeyboardButton.WithCallbackData(EmojiConsts.BrokenHeart, $"{reqOrResp} {profileUser.UserId.ToString()} false"),
                },
            });
        }

        public static Like ExtractLike(string callbackData, Like existingLikeForAnswer = null)
        {
            string[] args = callbackData.Split(' ');

            bool isRequest = args[0] == "req";
            int userId = int.Parse(args[1]);
            bool isLiked = bool.Parse(args[2]);

            if (existingLikeForAnswer != null)
            {
                if (isRequest)
                    throw new ArgumentException(nameof(existingLikeForAnswer) + " != null, но это реквест.");

                existingLikeForAnswer.Response = isLiked;
                return existingLikeForAnswer;
            }

            if (!isRequest)
                throw new ArgumentException(nameof(existingLikeForAnswer) + " == null, но это респонс.");

            var userContext = Container.Current.Resolve<UserContext>();
            return new Like(userContext.GetByUserId(userId), isLiked);
        }
    }
}
