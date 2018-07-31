using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Model.Commands
{
    class NoCommand : Command
    {
        public override string Name => throw new NotImplementedException();

        public override async Task Execute(Message Message)
        {
            var client = await BotWorker.Get();
            await client.SendTextMessageAsync(Message.Chat.Id, "Нет такой команды, товарищ!");
        }
    }
}
