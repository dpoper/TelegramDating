using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Models.Commands
{
    class ResetCommand : Command
    {
        public override string Name => "/reset";

        public override async Task Execute(Message Message, TelegramBotClient Client)
        {
            User currentUser = Database.Get(Message.Chat.Id);
            currentUser.State = (int) State.Create.Name;
            Database.Submit();

            await Client.SendTextMessageAsync(Message.Chat.Id, "Давай заново. Как мне к тебе обращаться?");
        }
    }
}
