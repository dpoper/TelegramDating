using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramDating.Model.StateMachine
{
    internal class StateSex : State, IGotCallbackQuery
    {
        public override async Task Handle(User currentUser, EventArgs callbackArgs)
        {
            var client = await BotWorker.Get();
            var callback = (callbackArgs as CallbackQueryEventArgs).CallbackQuery;

            if (callback.Data == "m") currentUser.Sex = true;
            else currentUser.Sex = false;

            // Enter Name
            await client.SendTextMessageAsync(callback.From.Id, "Хорошо! А как тебя зовут?");

            currentUser.State = new StateName();
        }
    }
}
