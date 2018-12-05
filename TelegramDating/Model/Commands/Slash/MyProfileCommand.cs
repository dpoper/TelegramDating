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

        public override async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            var message = msgOrCallback.ToMessage();

            await Program.Bot.SendPhotoAsync(
                chatId: message.From.Id,
                photo: currentUser.PictureId,
                caption: MessageFormatter.FormatProfileMessage(currentUser), 
                parseMode: ParseMode.Html);
        }
    }
}
