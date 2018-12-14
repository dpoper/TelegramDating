using Telegram.Bot.Types;
using TelegramDating.Enums;

namespace TelegramDating.Bot.Commands.AskActions
{
    internal class AskAge : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Age;

        public override async void Ask(Model.User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "А сколько тебе лет?");
        }

        public override bool Validate(Model.User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return AskAction.BaseTextValidation(cquery, message) && byte.TryParse(message.Text, out _);
        }

        public override async void OnValidationFail(Model.User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Пришли мне цифру!");
        }
    }
}
