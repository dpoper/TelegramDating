using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.ChatActions
{
    public abstract class ChatAction : Command
    {
        public abstract int Id { get; }

        public abstract Type NextAction { get; }

        public override async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            await this.HandleResponse(currentUser, msgOrCallback);
            await this.HandleAfter(currentUser, msgOrCallback);

            if (this.NextAction != null)
                currentUser.ChatActionId = (int)this.NextAction.ToEnum();

            await this.DbContext.SaveChangesAsync();
        }
        
        protected abstract Task HandleResponse(User currentUser, EventArgs msgOrCallback);

        protected abstract Task HandleAfter(User currentUser, EventArgs msgOrCallback);
    }
}