using TelegramDating.Enums;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.AskActions
{
    internal class AskName : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Name;

        public override async void Ask(User currentUser)
        {
            await BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "Как тебя зовут?");
        }
    }
}
