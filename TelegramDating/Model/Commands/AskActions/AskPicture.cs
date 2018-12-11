using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskPicture : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Picture;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId,
                "Отлично! Последний штрих – твоё фото. Пришли мне изображение или ссылку на картинку.");
        }

        public override bool Validate(User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            if (cquery != null)
                return false;

            if (message.Type == MessageType.Text && AskAction.BaseTextValidation(cquery, message))
            {
                return Uri.IsWellFormedUriString(message.Text, UriKind.Absolute);
            }
            else if (message.Type == MessageType.Photo)
            {
                return true;
            }

            return false;   
        }

        public override async void OnValidationFail(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId,
                "Пришли мне изображение или ссылку на картинку!!");
        }
    }
}
