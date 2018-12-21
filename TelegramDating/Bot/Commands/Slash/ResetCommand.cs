using System;
using System.ComponentModel;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.Slash
{
    [Description("начать всё с чистого листа!")]
    internal class ResetCommand : SlashCommand
    {
        public override string SlashText => "/reset";

        public override async void Execute(User currentUser, string @params = "")
        {
            await this.BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "Сбрасываем твой аккаунт...");

            currentUser.DeletedAt = DateTime.Now;
            var newUser = new User(currentUser.UserId, currentUser.Username);
            this.UserContext.Users.Add(newUser);
            this.UserContext.SaveChanges();

            this.BotWorker.FindAskAction(0).Ask(newUser);
        }
    }
}
