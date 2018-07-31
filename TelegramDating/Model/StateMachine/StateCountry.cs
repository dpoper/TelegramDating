using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramDating.Model.StateMachine
{
    internal class StateCountry : State
    {
        public override async Task Handle(User currentUser, EventArgs update)
        {
            var client = await BotWorker.Get(Program.Token);
            var message = (update as MessageEventArgs).Message;
            
            currentUser.Country = message.Text;
            
            await client.SendTextMessageAsync(message.Chat.Id, "Хорошо! А город?");
            currentUser.State = new StateCity();
        }
    }
}