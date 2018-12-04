using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionPicture : ChatAction
    {
        public override int Id => 6;

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
                InlineKeyboardButton.WithCallbackData("Мальчика"),
                InlineKeyboardButton.WithCallbackData("Девочку"),
                InlineKeyboardButton.WithCallbackData("Без разницы")
            });

            await Program.Bot.SendTextMessageAsync(
                messageArgs.ToMessage().Chat.Id,
                "Так, а кого искать тебе будем?",
                replyMarkup: sexKeyboard
            );
        }
    }
}