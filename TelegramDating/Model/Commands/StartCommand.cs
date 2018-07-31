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
            var database = UserRepository.Initialize();

            if (database.Contains(message.Chat.Username))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "ты уже в базе!");
                return;
            }

            var currentUser = new User(message.From.Id, message.From.Username);
            database.Add(currentUser);

            var user = database.Get(message.From.Id);

            user.State = new StateMachine.StateHello();
            await user.HandleState(System.EventArgs.Empty);
        }
    }
}
