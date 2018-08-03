using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionSearchSex : IChatAction, IGotCallbackQuery
    {
        public int Id => 7;

        public async Task Execute(User currentUser, EventArgs callbackArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var callback = (callbackArgs as CallbackQueryEventArgs)?.CallbackQuery;
            if (callback == null) return;
            
            switch (callback.Data)
            {
                case "m": currentUser.SearchSex = SearchOptions.Sex.Male;
                    break;
                case "f": currentUser.SearchSex = SearchOptions.Sex.Female;
                    break;
                default:  currentUser.SearchSex = SearchOptions.Sex.Any;
                    break;
            }

            currentUser.ChatActionId = new ActionSearchShow().Id;

            await client.SendTextMessageAsync(callback.From.Id,
                $"Чудесно, {currentUser.Name}! " +
                $"Теперь перейдём к поиску :)");

            await client.SendTextMessageAsync(callback.From.Id,
                "*типа поисковая панель и анкета рандом юзера с фоткой*");

            userRepo.Submit();
        }
    }
}