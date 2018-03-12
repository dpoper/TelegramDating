using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Models.Commands
{
    class ResetCommand : Command
    {
        public override string Name => "/reset";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            // Set state to Create.Name
        }
    }
}
