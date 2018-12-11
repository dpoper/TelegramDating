using System.Linq;
using Telegram.Bot.Types;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskAge : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Age;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "А сколько тебе лет?");
        }

        public override bool Validate(User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return AskAction.BaseTextValidation(cquery, message) && message.Text.All(char.IsDigit);
        }

        public override async void OnValidationFail(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Пришли мне цифру!");
        }
    }
}
