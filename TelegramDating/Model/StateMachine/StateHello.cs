using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDating.Model.StateMachine
{
    internal class StateHello : State
    {
        public override async Task Handle(User currentUser, EventArgs update)
        {
            var client = await BotWorker.Get(Program.Token);
            var message = (update as MessageEventArgs).Message;

            // Start
            var sexKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Мальчик", "m"),
                        InlineKeyboardButton.WithCallbackData("Девочка", "f")
                    });

            await client.SendTextMessageAsync(
                        message.Chat.Id,
                        "А кто ты у нас?",
                        replyMarkup: sexKeyboard
                        );

            currentUser.State = new StateSex();
        }
    }
}
