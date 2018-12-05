using TelegramDating.Model;

namespace TelegramDating.Global
{
    public static class MessageFormatter
    {
        public const string ProfileMessageTemplate = "<b>{0}, {1}</b> — {2}, {3}\n\n" +
                                                     "{{some info about this user}}\n" +
                                                     "Lorem Ipsum is simply dummy text of the printing " +
                                                     "and typesetting industry. Lorem Ipsum has been " +
                                                     "the industry's standard dummy text ever since the 1500s.";

        public static string FormatProfileMessage(User user)
        {
            return string.Format(ProfileMessageTemplate, // user.About
                user.Name, user.Age, user.Country, user.City);
        }
    }
}
