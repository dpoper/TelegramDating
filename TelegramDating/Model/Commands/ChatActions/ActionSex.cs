using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionSex : ChatAction, IGotCallbackQuery
    {
        public override int Id => 1;

        public override Type NextAction => typeof(ActionSearchSex);

        /// <inheritdoc />
        protected override async Task HandleResponse(User currentUser, EventArgs callbackArgs)
        {
            currentUser.Sex = callbackArgs.ToCallback()?.Data == "m";
        }

        /// <inheritdoc />
        protected override async Task HandleAfter(User currentUser, EventArgs callbackArgs)
        {
            await Program.Bot.SendTextMessageAsync(callbackArgs.ToCallback().From.Id, "Хорошо! А как тебя зовут?");
            
        }
    }
}
