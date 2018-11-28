using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramDating.Model.Commands.ChatActions;

namespace TelegramDating.Model.Commands.Slash
{
    class ResetCommand : SlashCommand
    {
        public override string SlashText => "/reset";

        public override async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            currentUser = null;

            var message = msgOrCallback.ToMessage();
            
            currentUser = new User(message.From.Id, message.From.Username);
            this.DbContext.Users.Add(currentUser);

            var user = this.DbContext.Users.SingleOrDefault(u => u.Id == message.From.Id);

            user.ChatActionId = new ActionHello().Id;
            this.DbContext.SaveChanges();

            await user.HandleAction(msgOrCallback);
        }
    }
}
