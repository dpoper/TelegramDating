﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskSex : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Sex;

        public override async void Ask(User currentUser)
        {
            var sexKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Мальчик", ((int)SearchOptions.Sex.Male).ToString()),
                InlineKeyboardButton.WithCallbackData("Девочка", ((int)SearchOptions.Sex.Female).ToString())
            });

            await Program.Bot.SendTextMessageAsync(
                currentUser.UserId,
                "А кто ты у нас?",
                replyMarkup: sexKeyboard
            );
        }

        public override async void After(User currentUser, CallbackQuery cquery, Message message = null)
        {
            await Program.Bot.EditMessageReplyMarkupAsync(cquery.Message.Chat.Id, cquery.Message.MessageId, replyMarkup: null);
        }
    }
}
