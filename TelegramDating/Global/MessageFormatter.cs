using TelegramDating.Model;

namespace TelegramDating.Global
{
    public static class MessageFormatter
    {
        public const string ProfileMessageTemplate = "<b>{0}, {1}</b> — {2}, {3}\n\n{4}";

        public static string FormatProfileMessage(User user)
        {
            return string.Format(ProfileMessageTemplate,
                user.Name, user.Age, user.Country, user.City, user.About);
        }
    }
}
