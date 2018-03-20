using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDating.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message Message, TelegramBotClient Client)
        {
            if (Database.Contains(Message.Chat.Username))
            {
                await Client.SendTextMessageAsync(Message.Chat.Id, "толян ты ебанулся чтоли, ты уже в базе");
                return;
            }

            var currentUser = new User(Message.From.Id, Message.From.Username);
            Database.Add(currentUser);

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
