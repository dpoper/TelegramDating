using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionHello : ChatAction
    {
        public override int Id => (int) ChatActionEnum.ActionHello;

        public override Type NextAction => typeof(ActionSex);

        /// <inheritdoc />
        protected override async Task HandleResponse(User currentUser, EventArgs msgOrCallback) { return; }

        protected override async Task HandleAfter(User currentUser, EventArgs messageArgs)
        {
            var message = messageArgs.ToMessage();
            if (message == null)
                return;
            
            var sexKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Мальчик", "m"),
                InlineKeyboardButton.WithCallbackData("Девочка", "f")
            });

            await Program.Bot.SendTextMessageAsync(
                message.Chat.Id,
                "А кто ты у нас?",
                replyMarkup: sexKeyboard
            );
        }
    }
}