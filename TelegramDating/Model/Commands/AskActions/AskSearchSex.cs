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

        public override async void After(User currentUser, CallbackQuery cquery, Message message = null)
        {
            await Program.Bot.EditMessageReplyMarkupAsync(cquery.Message.Chat.Id, cquery.Message.MessageId, replyMarkup: null);
        }
    }
}
