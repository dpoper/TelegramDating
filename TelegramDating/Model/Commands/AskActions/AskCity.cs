using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskCity : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.City;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "А город?");
        }
    }
}
