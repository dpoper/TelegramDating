﻿using System.ComponentModel;
using Telegram.Bot.Types.Enums;
using TelegramDating.Extensions;

namespace TelegramDating.Model.Commands.Slash
{
    [Description("показать твою анкету")]
    internal class MyProfileCommand : SlashCommand
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
