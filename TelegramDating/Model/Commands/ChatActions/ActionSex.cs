using System;
using System.Threading.Tasks;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionSex : ChatAction, IGotCallbackQuery
    {
        public override int Id => (int) ChatActionEnum.ActionSex;

        public override Type NextAction => typeof(ActionName);
        
        protected override async Task HandleResponse(User currentUser, EventArgs callbackArgs)
        {
            currentUser.Sex = callbackArgs.ToCallback()?.Data == "m";
        }
        
        protected override async Task HandleAfter(User currentUser, EventArgs callbackArgs)
        {
            await Program.Bot.SendTextMessageAsync(callbackArgs.ToCallback().From.Id, "Хорошо! А как тебя зовут?");
            
        }
    }
}
