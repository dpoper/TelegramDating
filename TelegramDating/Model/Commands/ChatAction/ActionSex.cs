using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionSex : IChatAction, IGotCallbackQuery
    {
        public int Id => 1;

        public async Task Execute(User currentUser, EventArgs callbackArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var callback = (callbackArgs as CallbackQueryEventArgs)?.CallbackQuery;
            if (callback == null) return;
            

            if (callback.Data == "m") currentUser.Sex = true;
            else currentUser.Sex = false;

            // Enter Name
            await client.SendTextMessageAsync(callback.From.Id, "Хорошо! А как тебя зовут?");

            currentUser.ChatActionId = new ActionName().Id;
            userRepo.Submit();
        }
    }
}
