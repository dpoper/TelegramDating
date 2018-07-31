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
            var client = await BotWorker.Get();
            var user = UserRepository.Initialize().Get(message.Chat.Id);
            user.State = new StateMachine.StateHello();
            await user.HandleState(System.EventArgs.Empty);
        }
    }
}
