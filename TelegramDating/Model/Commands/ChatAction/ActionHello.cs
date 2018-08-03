using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionHello : IChatAction
    {
        public int Id => 0;

        public async Task Execute(User currentUser, EventArgs messageEvArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var message = (messageEvArgs as MessageEventArgs)?.Message;
            if (message == null) return;

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

            currentUser.ChatActionId = new ActionSex().Id;
            userRepo.Submit();
        }
    }
}