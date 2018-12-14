using System.ComponentModel;

namespace TelegramDating.Model.Commands.Slash
{
    [Description("начать всё с чистого листа!")]
    internal class ResetCommand : SlashCommand
    {
        public override string SlashText => "/reset";

        public override async void Execute(User currentUser, string @params = "")
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Сбрасываем твой аккаунт...");
            
            currentUser = new User(currentUser.UserId, currentUser.Username);
            this.UserContext.SaveChanges();

            BotWorker.FindAskAction(0).Ask(currentUser);
        }
    }
}
