using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message message)
        {
            var client = await BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            if (userRepo.Contains(message.Chat.Username))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "ты уже в базе!");
                return;
            }

            var currentUser = new User(message.From.Id, message.From.Username);
            userRepo.Add(currentUser);

            var user = userRepo.Get(message.From.Id);

            user.State = new StateMachine.StateHello();
            await user.HandleState(System.EventArgs.Empty);
        }
    }
}
