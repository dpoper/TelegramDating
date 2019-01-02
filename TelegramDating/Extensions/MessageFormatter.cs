﻿using TelegramDating.Model;

namespace TelegramDating.Extensions
{
    public static class MessageFormatter
    {
        public const string ForResponseTemplateText = "Ты кому-то понравился! Вот его анкета.";

        public const string ProfileMessageTemplateHtml = "<b>{0}, {1}</b> — {2}, {3}\n\n{4}";

		public const string MentionTemplateHtml = "<a href=\"tg://user?id={0}\">{1}</a>";

        public static string FormatProfileMessage(User user, bool isForResponse = false)
        {
            return (isForResponse ? ForResponseTemplateText + "\n\n" : "") 
				+ string.Format(MessageFormatter.ProfileMessageTemplateHtml,
                user.Name, user.Age, user.Country, user.City, user.About);
        }

		public static string GetMentionText(Telegram.Bot.Types.User tgUser)
		{
			int userId = tgUser.Id;
			string displayedName = tgUser.FirstName ?? tgUser.LastName ?? tgUser.Username ?? "User";

			return string.Format(MessageFormatter.MentionTemplateHtml, userId.ToString(), displayedName);
		}
    }
}
