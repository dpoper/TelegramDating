
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.Slash
{
    internal class NoCommand : SlashCommand
    {
        public override string SlashText => null;

        public override async void Execute(User currentUser, string @params = "")
        {
            await this.BotWorker.Instance.SendTextMessageAsync(currentUser.UserId, "Нет такой команды, товарищ!");
        }
    }
}
