using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.StateMachine
{
    internal class StateSearchSex : State, IGotCallbackQuery
    {
        public override async Task Handle(User currentUser, EventArgs callbackArgs)
        {
            var client = await BotWorker.Get();
            var callback = (callbackArgs as CallbackQueryEventArgs).CallbackQuery;

            if (callback.Data == "m")
                currentUser.SearchSex = SearchOptions.Sex.Male;
            else if (callback.Data == "f")
                currentUser.SearchSex = SearchOptions.Sex.Female;
            else
                currentUser.SearchSex = SearchOptions.Sex.Any;

            currentUser.State = new StateSearchShow();

            await client.SendTextMessageAsync(callback.From.Id,
                $"Чудесно, {currentUser.Name}! " +
                $"Теперь перейдём к поиску :)");

            await client.SendTextMessageAsync(callback.From.Id,
                "*типа поисковая панель и анкета рандом юзера с фоткой*");
        }
    }
}