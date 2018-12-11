using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskSearchSex : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.SearchSex;

        public override async void Ask(User currentUser)
        {
            var sexKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] 
                {
                    InlineKeyboardButton.WithCallbackData("Мальчика", ((int)SearchOptions.Sex.Male).ToString()),
                    InlineKeyboardButton.WithCallbackData("Девочку", ((int)SearchOptions.Sex.Female).ToString()),
                },
                new[] { InlineKeyboardButton.WithCallbackData("Без разницы", ((int) SearchOptions.Sex.Any).ToString()) }
            });

            await Program.Bot.SendTextMessageAsync(
                currentUser.UserId,
                "Так, а кого искать тебе будем?",
                replyMarkup: sexKeyboard
            );
        }

        public override bool Validate(User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return message == null
                   && cquery != null
                   &&    (cquery.Data == ((int) SearchOptions.Sex.Male).ToString()
                       || cquery.Data == ((int) SearchOptions.Sex.Female).ToString()
                       || cquery.Data == ((int) SearchOptions.Sex.Any).ToString());
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
