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
            var message = messageArgs.ToMessage();

            var foundUser = this.DbContext.Users.SingleOrDefault(u => u.UserId == message.Chat.Id);
            if (foundUser != null)
            {
                await Program.Bot.SendTextMessageAsync(message.Chat.Id, 
                    "Ты уже существуешь!\n" +
                    "Используй /reset для того, чтобы пересоздать аккаунт.");

                //await Program.Bot.SendTextMessageAsync(message.Chat.Id, 
                //    "Но мы, кажется, остановились на том, что...");

                //foundUser.HandleAction(null);

                return;
            }
            
            currentUser = new User(message.From.Id, message.From.Username);
            this.DbContext.Users.Add(currentUser);
            this.DbContext.SaveChanges();
            
            await currentUser.HandleAction(messageArgs);
        }
    }
}
