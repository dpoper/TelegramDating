using System;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.Slash
{
    public class StartCommand : SlashCommand
    {
        public override string SlashText => "/start";

        /// <summary>
        /// Leave currentUser field null.
        /// </summary>
        /// <param name="currentUser">Leave it null.</param>
        public override async Task Execute(User currentUser, EventArgs messageArgs)
        {
            currentUser = null;

            var message = messageArgs.ToMessage();

            Console.WriteLine(message.Chat.Id);
            if (this.DbContext.Users.SingleOrDefault(u => u.UserId == message.Chat.Id) != null)
            {
                await Program.Bot.SendTextMessageAsync(message.Chat.Id, "ты уже в базе!");
                return;
            }

            // Create user
            currentUser = new User(message.From.Id, message.From.Username);
            this.DbContext.Users.Add(currentUser);
            this.DbContext.SaveChanges();

            // var user = this.DbContext.Users.SingleOrDefault(u => u.Id == message.From.Id);
            await currentUser.HandleAction(messageArgs);
        }
    }
}
