
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.Slash
{
    internal class NoCommand : SlashCommand
    {
        public override string SlashText => null;

        public override async void Execute(User currentUser, string @params = "")
        {
            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Нет такой команды, товарищ!");
        }
    }
}
