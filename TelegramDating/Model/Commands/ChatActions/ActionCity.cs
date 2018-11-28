using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionCity : ChatAction
    {
        public override int Id => 5;

        public override Type NextAction => typeof(ActionPicture);

        protected override async Task HandleResponse(User currentUser, EventArgs messageArgs)
        {
            var message = messageArgs.ToMessage();
            if (message == null) return;

            currentUser.City = message.Text;
        }

        protected override async Task HandleAfter(User currentUser, EventArgs messageArgs)
        {
            await Program.Bot.SendTextMessageAsync(messageArgs.ToMessage().Chat.Id,
                "Отлично! Последний штрих – твоё фото. Пришли мне картинку.");
        }
    }
}

