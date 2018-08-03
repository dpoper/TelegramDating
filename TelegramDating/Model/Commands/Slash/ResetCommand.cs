using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;
using TelegramDating.Model.Commands.Bot;

namespace TelegramDating.Model.Commands.Slash
{
    class ResetCommand : ISlashCommand
    {
        public string SlashText => "/reset";

        public async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            currentUser = null;

            var message = (msgOrCallback as MessageEventArgs).Message;

            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            currentUser = new User(message.From.Id, message.From.Username);
            userRepo.Add(currentUser);

            var user = userRepo.Get(message.From.Id);

            user.ChatActionId = new ActionHello().Id;
            userRepo.Submit();

            await user.HandleAction(msgOrCallback);
        }
    }
}
