
namespace TelegramDating.Model.Commands.Slash
{
    class ResetCommand : SlashCommand
    {
        public override string SlashText => "/reset";

        public override async void Execute(User currentUser, string @params = "")
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Сбрасываем твой аккаунт...");

            this.UserContext.Users.Remove(currentUser);

            User createdUser = new User(currentUser.UserId, currentUser.Username);
            this.UserContext.Users.Add(createdUser);
            this.UserContext.SaveChanges();

            BotWorker.FindAskAction(0).Ask(currentUser);
        }
    }
}
