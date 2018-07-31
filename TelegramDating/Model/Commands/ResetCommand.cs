using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands
{
    class ResetCommand : Command
    {
        public override string Name => "/reset";

        public override async Task Execute(Message message)
        {
            var userRepo = UserRepository.Initialize();
            var client = await BotWorker.Get();
            var user = userRepo.Get(message.Chat.Id);

            user.State = new StateMachine.StateHello();
            userRepo.Submit();

            await user.HandleState(System.EventArgs.Empty);
        }
    }
}
