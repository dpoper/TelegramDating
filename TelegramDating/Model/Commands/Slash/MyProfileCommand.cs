using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using TelegramDating.Global;

namespace TelegramDating.Model.Commands.Slash
{
    class MyProfileCommand : SlashCommand
    {
        public override string SlashText => "/myprofile";

        public override async void Execute(User currentUser, string @params = "")
        {
            await Program.Bot.SendPhotoAsync(
                chatId: currentUser.UserId,
                photo: currentUser.PictureId,
                caption: MessageFormatter.FormatProfileMessage(currentUser), 
                parseMode: ParseMode.Html);
        }
    }
}
