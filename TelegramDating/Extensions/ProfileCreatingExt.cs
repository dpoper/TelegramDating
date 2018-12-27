using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramDating.Bot.Commands.AskActions;
using TelegramDating.Enums;

namespace TelegramDating.Extensions
{
    public static class ProfileCreatingExt
    {
        private static Assembly Assembly { get; } = typeof(AskAction).Assembly;

        public static ProfileCreatingEnum ToEnum(this Type askAction)
        {
            return (ProfileCreatingEnum) Enum.Parse(typeof(ProfileCreatingEnum), askAction.Name.Replace("Ask", ""));
        }

        public static Type ToType(this ProfileCreatingEnum profileCreatingEnum)
        {
            return Assembly.DefinedTypes.SingleOrDefault(t => t.Name == "Ask" + profileCreatingEnum.ToString());
        }

        public static int IndexOf(this IList<AskAction> askActionList, ProfileCreatingEnum profileCreatingEnum)
        {
            for (int index = 0; index < askActionList.Count; index++)
            {
                if (askActionList.ElementAt(index).Id == (int)profileCreatingEnum)
                    return index;
            }

            throw new KeyNotFoundException();
        }
    }
}
