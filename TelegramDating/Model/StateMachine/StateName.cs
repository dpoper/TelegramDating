using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramDating.Model.StateMachine
{
    internal class StateName : State
    {
        public override async Task Handle(User currentUser, EventArgs msgArgs)
        {
            var client = await BotWorker.Get();
            var message = (msgArgs as MessageEventArgs).Message;

            currentUser.Name = message.Text;

            // Enter Age
            await client.SendTextMessageAsync(message.Chat.Id,
                            $"Приятно познакомиться, {currentUser.Name}! " +
                            $"А сколько тебе лет?");

            currentUser.State = new StateAge();
        }
    }
}