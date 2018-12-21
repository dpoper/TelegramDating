using Telegram.Bot.Types;
using TelegramDating.Enums;
using TelegramDating.Extensions;

namespace TelegramDating.Bot.Commands.AskActions
{
    internal class AskSearchSex : AskAction, IGotCallbackQuery
    {
        public override int Id => (int) ProfileCreatingEnum.SearchSex;

        public override async void Ask(Model.User currentUser)
        {
            await this.BotWorker.Instance.SendTextMessageAsync(
                currentUser.UserId,
                "Так, а кого искать тебе будем?",
                replyMarkup: CallbackKeyboardExt.SearchSex
            );
        }

        public override bool Validate(Model.User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return message == null
                   && cquery != null
                   &&    (cquery.Data == ((int) SearchOptions.Sex.Male).ToString()
                       || cquery.Data == ((int) SearchOptions.Sex.Female).ToString()
                       || cquery.Data == ((int) SearchOptions.Sex.Any).ToString());
        }

        public override async void OnValidationFail(Model.User currentUser)
        {
            await this.BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "Просто нажми чёртову кнопку!");
        }

        public override void OnSuccess(Model.User currentUser, CallbackQuery cquery, Message message = null)
        {
            this.BotWorker.RemoveKeyboard(cquery);
        }
    }
}
