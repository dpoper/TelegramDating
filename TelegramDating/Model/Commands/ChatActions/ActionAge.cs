using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionAge : ChatAction
    {
        public override int Id => 3;
        
        public override Type NextAction => typeof(ActionCountry);

        /// <inheritdoc />
        protected override async Task HandleResponse(User currentUser, EventArgs messageArgs)
        {
            var message = messageArgs.ToMessage();
            if (message == null) return;

            if (!(int.TryParse(message.Text, out int age) && age > 0))
            {
                await Program.Bot.SendTextMessageAsync(message.Chat.Id, "что-то не так! давай-ка ещё раз");
                return;
            }
            currentUser.Age = age;
        }

        /// <inheritdoc />
        protected override async Task HandleAfter(User currentUser, EventArgs messageArgs)
        {
            var message = messageArgs.ToMessage();
            if (message == null) return;

            await Program.Bot.SendTextMessageAsync(message.Chat.Id, "Ого! совсем уже взрослый. А ты из какой страны?");
        }
    }
}