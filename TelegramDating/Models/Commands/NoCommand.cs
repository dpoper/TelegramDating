using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Models.Commands
{
    class NoCommand : Command
    {
        public override string Name => throw new NotImplementedException();

        public override async Task Execute(Message Message, TelegramBotClient Client)
            => await Client.SendTextMessageAsync(Message.Chat.Id, "Нет такой команды, товарищ!");
    }
}
