using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskSex : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Sex;

        public override async void Ask(User currentUser)
        {
            var sexKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithCallbackData("Мальчик", ((int)SearchOptions.Sex.Male).ToString()),
                InlineKeyboardButton.WithCallbackData("Девочка", ((int)SearchOptions.Sex.Female).ToString())
            });

            await Program.Bot.SendTextMessageAsync(
                currentUser.UserId,
                "А кто ты у нас?",
                replyMarkup: sexKeyboard
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

        public override async void OnSuccess(User currentUser, CallbackQuery cquery, Message message = null)
        {
            await Program.Bot.EditMessageReplyMarkupAsync(cquery.Message.Chat.Id, cquery.Message.MessageId, replyMarkup: null);
        }
    }
}
