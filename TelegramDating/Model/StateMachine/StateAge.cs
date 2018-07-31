using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.StateMachine
{
    internal class StateAge : State
    {
        public override async Task Handle(User currentUser, EventArgs update)
        {
            var client = await BotWorker.Get(Program.Token);
            var message = (update as MessageEventArgs).Message;
            var userRepo = UserRepository.Initialize();

            int Age;
            if (!(int.TryParse(message.Text, out Age) && Age > 0))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "что-то не так! давай-ка ещё раз");
                return;
            }
            currentUser.Age = Age;
            
            await client.SendTextMessageAsync(message.Chat.Id, "Ого! совсем уже взрослый. А ты из какой страны?");
            currentUser.State = new StateCountry();
            userRepo.Submit();
        }
    }
}