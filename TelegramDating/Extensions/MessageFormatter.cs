using TelegramDating.Model;

namespace TelegramDating.Extensions
{
    public static class MessageFormatter
    {
        public const string ForResponseTemplate = "Ты кому-то понравился! Вот его анкета.";

        public const string ProfileMessageTemplate = "<b>{0}, {1}</b> — {2}, {3}\n\n{4}";

        public static string FormatProfileMessage(User user, bool isForResponse = false)
        {
            return (isForResponse ? ForResponseTemplate + "\n\n" : "") + string.Format(ProfileMessageTemplate,
                user.Name, user.Age, user.Country, user.City, user.About);
        }
    }
}
