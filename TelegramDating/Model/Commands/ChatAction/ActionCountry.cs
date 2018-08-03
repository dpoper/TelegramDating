using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionCountry : IChatAction
    {
        public int Id => 4;

        public async Task Execute(User currentUser, EventArgs messageEvArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var message = (messageEvArgs as MessageEventArgs)?.Message;
            if (message == null) return;

            currentUser.Country = message.Text;
            
            await client.SendTextMessageAsync(message.Chat.Id, "Хорошо! А город?");
            currentUser.ChatActionId = new ActionCity().Id;
            userRepo.Submit();
        }
    }
}