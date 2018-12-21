using System.ComponentModel;
using Telegram.Bot.Types.Enums;
using TelegramDating.Extensions;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.Slash
{
    [Description("показать твою анкету")]
    internal class MyProfileCommand : SlashCommand
    {
        public override string SlashText => "/myprofile";

        public override async void Execute(User currentUser, string @params = "")
        {
            if (currentUser.IsCreatingProfile())
            {
                await this.BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "Сперва заполни анкету полностью!");
                return;
            }

            await this.BotWorker.Instance.SendPhotoAsync(
                chatId: currentUser.UserId,
                photo: currentUser.PictureId,
                caption: MessageFormatter.FormatProfileMessage(currentUser), 
                parseMode: ParseMode.Html);
        }
    }
}
