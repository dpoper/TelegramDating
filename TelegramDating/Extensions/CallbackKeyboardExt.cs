using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Database;
using TelegramDating.Shared;
using TelegramDating.Model;
using TelegramDating.Enums;

namespace TelegramDating.Extensions
{
    internal static class CallbackKeyboardExt
    {
        private static UserContext UserContext { get; set; } = Container.Current.Resolve<UserContext>();

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



        public static InlineKeyboardMarkup CreateLikeDislikeKeyboard(User foundUser)
        {
            return new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(EmojiConsts.Heart, $"true {foundUser.UserId}"),
                    InlineKeyboardButton.WithCallbackData(EmojiConsts.BrokenHeart, $"false {foundUser.UserId}"),
                },
            });
        }

        public static Like ExtractLike(User currentUser, string callbackData)
        {
            string[] args = callbackData.Split(' ');

            bool liked = bool.Parse(args[0]);
            int userId = int.Parse(args[1]);

            return new Like(UserContext.GetByUserId(userId), liked);
        }
    }
}
