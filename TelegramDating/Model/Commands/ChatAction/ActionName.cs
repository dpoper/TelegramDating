using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionName : IChatAction
    {
        public int Id => 2;

        public async Task Execute(User currentUser, EventArgs msgArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var message = (msgArgs as MessageEventArgs)?.Message;
            if (message == null) return;

            currentUser.Name = message.Text;

            // Enter Age
            await client.SendTextMessageAsync(message.Chat.Id,
                            $"Приятно познакомиться, {currentUser.Name}! " +
                            $"А сколько тебе лет?");

            currentUser.ChatActionId = new ActionAge().Id;
            userRepo.Submit();
        }
    }
}