using System;
using System.Threading.Tasks;
using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionSearchSex : ChatAction, IGotCallbackQuery
    {
        public override int Id => 7;
        
        public override Type NextAction => typeof(ActionSearchShow);

        protected override async Task HandleResponse(User currentUser, EventArgs callbackArgs)
        {
            var callback = callbackArgs.ToCallback();
            if (callback == null) return;

            switch (callback.Data)
            {
                case "m":
                    currentUser.SearchSex = SearchOptions.Sex.Male;
                    break;
                case "f":
                    currentUser.SearchSex = SearchOptions.Sex.Female;
                    break;
                default:
                    currentUser.SearchSex = SearchOptions.Sex.Any;
                    break;
            }
        }

        protected override async Task HandleAfter(User currentUser, EventArgs callbackArgs)
        {
            var callback = callbackArgs.ToCallback();
            if (callback == null) return;

            await Program.Bot.SendTextMessageAsync(callback.From.Id,
                $"Чудесно, {currentUser.Name}! " +
                $"Теперь перейдём к поиску :)");

            await Program.Bot.SendTextMessageAsync(callback.From.Id,
                "*типа поисковая панель и анкета рандом юзера с фоткой*");
        }
    }
}