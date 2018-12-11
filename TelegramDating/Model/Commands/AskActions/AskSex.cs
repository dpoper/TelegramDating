using Telegram.Bot.Types;
using TelegramDating.Extensions;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskSex : AskAction, IGotCallbackQuery
    {
        public override int Id => (int) ProfileCreatingEnum.Sex;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(
                currentUser.UserId,
                "А кто ты у нас?",
                replyMarkup: CallbackKeyboardExt.Sex
            );
        }

        public override bool Validate(User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return message == null
                   && cquery != null
                   &&    (cquery.Data == ((int) SearchOptions.Sex.Male).ToString()
                       || cquery.Data == ((int) SearchOptions.Sex.Female).ToString());
        }

        public override async void OnValidationFail(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Просто нажми чёртову кнопку!");
        }

        public override void OnSuccess(User currentUser, CallbackQuery cquery, Message message = null)
        {
            BotWorker.RemoveKeyboard(cquery);
        }
    }
}
