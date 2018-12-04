using System;
using System.Threading.Tasks;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.Slash
{
    class ResetCommand : SlashCommand
    {
        public override string SlashText => "/reset";

        public override async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            var message = msgOrCallback.ToMessage();

            await Program.Bot.SendTextMessageAsync(message.From.Id, "Сбрасываем твой аккаунт...");

            this.DbContext.Users.Remove(currentUser);

            var createdUser = new User(message.From.Id, message.From.Username);
            this.DbContext.Users.Add(createdUser);

            createdUser.ChatActionId = (int)ChatActionEnum.ActionHello;
            this.DbContext.SaveChanges();

            await createdUser.HandleAction(msgOrCallback);
        }
    }
}
