using System;
using System.Threading.Tasks;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionCountry : ChatAction
    {
        public override int Id => (int) ChatActionEnum.ActionCountry;

        public override Type NextAction => typeof(ActionCity);
        
        protected override async Task HandleResponse(User currentUser, EventArgs messageArgs)
        {
            currentUser.Country = messageArgs.ToMessage().Text;
        }

        protected override async Task HandleAfter(User currentUser, EventArgs messageArgs)
        {
            await Program.Bot.SendTextMessageAsync(messageArgs.ToMessage().Chat.Id, "Хорошо! А город?");
        }
    }
}