using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Global;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionSearchShow : ChatAction, IGotCallbackQuery
    {
        public override int Id => (int) ChatActionEnum.ActionSearchShow;
        
        public override Type NextAction => null;

        protected override async Task HandleResponse(User currentUser, EventArgs msgOrCallback)
        {
            var callback = msgOrCallback.ToCallback();
            if (callback == null)
                return;

            switch (callback.Data)
            {
                case "y":
                    Console.WriteLine("Like");
                    break;
                case "n":
                    Console.WriteLine("Dislike");
                    break;
                default:
                    Console.WriteLine("Err");
                    break;
            }
        }

        protected override async Task HandleAfter(User currentUser, EventArgs msgOrCallback)
        {
            var callback = msgOrCallback.ToCallback();
            if (callback == null)
                return;

            var likeKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData(EmojiConsts.Heart, "y"),
                InlineKeyboardButton.WithCallbackData(EmojiConsts.BrokenHeart, "n"),
            });

            await Program.Bot.SendPhotoAsync(
                chatId: callback.From.Id,
                photo: currentUser.PictureId,
                caption: MessageFormatter.FormatProfileMessage(currentUser),
                parseMode: ParseMode.Html,
                replyMarkup: likeKeyboard);
        }
    }
}