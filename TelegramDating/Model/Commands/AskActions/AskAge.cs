using TelegramDating.Model.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    internal class AskAge : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.Age;

        public override async void Ask(User currentUser)
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "А сколько тебе лет?");
        }
    }
}
