using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskName : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Name;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Как тебя зовут?");
        }
    }
}
