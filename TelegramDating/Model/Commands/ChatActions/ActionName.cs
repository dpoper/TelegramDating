using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionName : ChatAction
    {
        public override int Id => 2;

        public override Type NextAction => typeof(ActionAge);

        /// <inheritdoc />
        protected override async Task HandleResponse(User currentUser, EventArgs messageArgs)
        {
            currentUser.Name = messageArgs.ToMessage()?.Text;
        }

        /// <inheritdoc />
        protected override async Task HandleAfter(User currentUser, EventArgs messageArgs)
        {
            await Program.Bot.SendTextMessageAsync(messageArgs.ToMessage().Chat.Id,
                $"Приятно познакомиться, {currentUser.Name}! " +
                $"А сколько тебе лет?");
        }
    }
}