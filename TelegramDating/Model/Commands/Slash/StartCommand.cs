
using System.ComponentModel;

namespace TelegramDating.Model.Commands.Slash
{
    [Description("с этого все начинали")]
    internal class StartCommand : SlashCommand
    {
        public override string SlashText => "/start";

        public override async void Execute(User currentUser, string @params = "")
        {
            var foundUser = this.UserContext.GetByUserId(currentUser.UserId);
            if (foundUser != null)
            {
                await Program.Bot.SendTextMessageAsync(foundUser.UserId, 
                    "Ты уже существуешь!\n" +
                    "Используй /reset для того, чтобы пересоздать аккаунт.");

                //await Program.Bot.SendTextMessageAsync(message.Chat.Id, "Но мы, кажется, остановились на том, что...");
                //foundUser.HandleAction(null);

                return;
            }

            this.UserContext.Users.Add(currentUser);
            this.UserContext.SaveChanges();

            await Program.Bot.SendTextMessageAsync(currentUser.UserId,
                "Привет!\n" +
                "Заполни простую анкету, чтобы мы могли найти для тебя кого-нибудь.");

            BotWorker.FindAskAction(0).Ask(currentUser);
        }
    }
}
