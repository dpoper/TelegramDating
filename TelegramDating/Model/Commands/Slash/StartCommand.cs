using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Slash
{
    public class StartCommand : ISlashCommand
    {
        public string SlashText => "/start";

        /// <summary>
        /// Leave currentUser field null.
        /// </summary>
        /// <param name="currentUser">Leave it null.</param>
        /// <param name="msgOrCallback"></param>
        /// <returns></returns>
        public async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            currentUser = null;

            var message = (msgOrCallback as MessageEventArgs).Message;

            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            Console.WriteLine(message.Chat.Id);
            if (userRepo.Contains(message.Chat.Id))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "ты уже в базе!");
                return;
            }

            // Create user
            currentUser = new User(message.From.Id, message.From.Username);
            userRepo.Add(currentUser);
            userRepo.Submit();

            var user = userRepo.Get(message.From.Id);
            await user.HandleAction(msgOrCallback);
        }
    }
}
