using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using TelegramDating.Extensions;
using TelegramDating.Shared;

namespace TelegramDating.Model.Commands.Slash
{
    class MyProfileCommand : SlashCommand
    {
        public override string SlashText => "/myprofile";

        public override async void Execute(User currentUser, string @params = "")
        {
            if (currentUser.IsCreatingProfile())
            {
                await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Сперва заполни анкету полностью!");
                return;
            }

            await Program.Bot.SendPhotoAsync(
                chatId: currentUser.UserId,
                photo: currentUser.PictureId,
                caption: MessageFormatter.FormatProfileMessage(currentUser), 
                parseMode: ParseMode.Html);
        }
    }
}
