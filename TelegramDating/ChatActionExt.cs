using System;
using System.Linq;
using System.Reflection;
using TelegramDating.Model.Commands.ChatActions;
using TelegramDating.Model.Enums;

namespace TelegramDating
{
    public static class ChatActionExt
    {
        private static Assembly Assembly { get; } = typeof(ChatAction).Assembly;

        public static ChatActionEnum ToEnum(this Type chatAction)
        {
            return (ChatActionEnum) Enum.Parse(typeof(ChatActionEnum), chatAction.Name);
        }

        public static Type ToType(this ChatActionEnum chatActionEnum)
        {
            return Assembly.GetTypes().SingleOrDefault(t => t.Name == chatActionEnum.ToString());
        }

        public static bool IsCallbackQueryAction(this Type chatAction)
        {
            return chatAction.GetInterfaces().Contains(typeof(IGotCallbackQuery));
        }

        public static bool IsCallbackQueryAction(this ChatActionEnum chatActionEnum)
        {
            return chatActionEnum.ToType().IsCallbackQueryAction();
        }
    }
}
