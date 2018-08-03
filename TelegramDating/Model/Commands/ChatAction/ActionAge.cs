using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionAge : IChatAction
    {
        public int Id => 3;

        public async Task Execute(User currentUser, EventArgs msgEvArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var message = (msgEvArgs as MessageEventArgs)?.Message;
            if (message == null) return;

            int Age;
            if (!(int.TryParse(message.Text, out Age) && Age > 0))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "что-то не так! давай-ка ещё раз");
                return;
            }
            currentUser.Age = Age;
            
            await client.SendTextMessageAsync(message.Chat.Id, "Ого! совсем уже взрослый. А ты из какой страны?");
            currentUser.ChatActionId = new ActionCountry().Id;
            userRepo.Submit();
        }
    }
}