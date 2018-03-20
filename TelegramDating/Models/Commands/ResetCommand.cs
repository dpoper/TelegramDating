using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDating.Models.Commands
{
    class ResetCommand : Command
    {
        public override string Name => "/reset";

        public override async Task Execute(Message Message, TelegramBotClient Client)
        {
            User currentUser = Database.Get(Message.Chat.Id);
            currentUser.State = (int) State.Create.Sex;
            Database.Submit();

            var sexKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Мальчик"),
                        InlineKeyboardButton.WithCallbackData("Девочка"),
                    });

            await Client.SendTextMessageAsync(
                        Message.Chat.Id,
                        "А кто ты у нас?",
                        replyMarkup: sexKeyboard
                        );
        }
    }
}
