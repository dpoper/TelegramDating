using TelegramDating.Enums;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.AskActions
{
    internal class AskAbout : AskAction
    {
        public override int Id => (int) ProfileCreatingEnum.About;

        public override async void Ask(User currentUser)
        {
            await this.BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "Теперь расскажи немного о себе.");
        }
    }
}
