using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Models.Commands
{
    class NoCommand : Command
    {
        public override string Name => throw new NotImplementedException();

        public override async Task Execute(Message message, TelegramBotClient client)
            => await client.SendTextMessageAsync(message.Chat.Id, "Нет такой команды, товарищ!");
    }
}
