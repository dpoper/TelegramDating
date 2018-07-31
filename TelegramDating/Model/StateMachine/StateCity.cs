using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramDating.Model.StateMachine
{
    internal class StateCity : State
    {
        public override async Task Handle(User currentUser, EventArgs update)
        {
            var client = await BotWorker.Get(Program.Token);
            var message = (update as MessageEventArgs).Message;
            
            currentUser.City = message.Text;
            
            await client.SendTextMessageAsync(message.Chat.Id, 
                "Отлично! Последний штрих – твоё фото. Пришли мне картинку.");

            currentUser.State = new StatePicture();
        }
    }
}