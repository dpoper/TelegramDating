using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramDating.Extensions
{
    public static class TelegramUpdateEventArgsExt
    {
        public static CallbackQuery ToCallbackQuery(this EventArgs callbackArgs)
        {
            return (callbackArgs as CallbackQueryEventArgs).CallbackQuery;
        }

        public static Message ToMessage(this EventArgs messageArgs)
        {
            return (messageArgs as MessageEventArgs).Message;
        }
    }
}
