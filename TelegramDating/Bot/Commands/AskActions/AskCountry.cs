using TelegramDating.Enums;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.AskActions
{
    internal class AskCountry : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Country;

        public override async void Ask(User currentUser)
        {
            await this.BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "А из какой ты страны?");
        }
    }
}
