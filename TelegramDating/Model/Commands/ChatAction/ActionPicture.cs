using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionPicture : IChatAction
    {
        public int Id => 6;

        public async Task Execute(User currentUser, EventArgs msgArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var message = (msgArgs as MessageEventArgs)?.Message;
            if (message == null) return;

            if (message.Type != MessageType.Photo)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "пришли фотку!!");
                return;
            }

            string pictureId = message.Photo.Last().FileId;
            currentUser.PictureId = pictureId;

            await client.SendTextMessageAsync(message.Chat.Id, "Это ты? Красивый...");


            var sexKeyboard = new InlineKeyboardMarkup(new[]
            {
                            InlineKeyboardButton.WithCallbackData("Мальчика"),
                            InlineKeyboardButton.WithCallbackData("Девочку"),
                            InlineKeyboardButton.WithCallbackData("Без разницы")
                        });

            await client.SendTextMessageAsync(
                       message.Chat.Id,
                       "Так, а кого искать тебе будем?",
                       replyMarkup: sexKeyboard
                        );

            currentUser.ChatActionId = new ActionSearchSex().Id;
            userRepo.Submit();
        }
    }
}