using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.Slash
{
    class NoCommand : SlashCommand
    {
        public override string SlashText => null;

        public override async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            await Program.Bot.SendTextMessageAsync(msgOrCallback.ToMessage().Chat.Id, 
                "Нет такой команды, товарищ!");
        }
    }
}
