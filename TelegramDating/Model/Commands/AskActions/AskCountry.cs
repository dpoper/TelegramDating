using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskCountry : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Country;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "А из какой ты страны?");
        }
    }
}
