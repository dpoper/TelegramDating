using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionPicture : ChatAction
    {
        public override int Id => (int) ChatActionEnum.ActionPicture;

        public override Type NextAction => typeof(ActionSearchSex);
        
        protected override async Task HandleResponse(User currentUser, EventArgs messageArgs)
        {
            var message = messageArgs.ToMessage();
            if (message == null) return;

            if (message.Type != MessageType.Photo)
            {
                await Program.Bot.SendTextMessageAsync(message.Chat.Id, "пришли фотку!!");
                return;
            }

            string pictureId = message.Photo.Last().FileId;
            currentUser.PictureId = pictureId;

            await Program.Bot.SendTextMessageAsync(message.Chat.Id, "Это ты? Красивый...");
        }

        protected override async Task HandleAfter(User currentUser, EventArgs messageArgs)
        {
            var sexKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Мальчика", "m"),
                InlineKeyboardButton.WithCallbackData("Девочку", "f"),
                InlineKeyboardButton.WithCallbackData("Без разницы", "a")
            });

            await Program.Bot.SendTextMessageAsync(
                messageArgs.ToMessage().Chat.Id,
                "Так, а кого искать тебе будем?",
                replyMarkup: sexKeyboard
            );
        }
    }
}