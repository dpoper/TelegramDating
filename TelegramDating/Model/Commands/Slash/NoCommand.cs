using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace TelegramDating.Model.Commands.Slash
{
    class NoCommand : ISlashCommand
    {
        public string SlashText => throw new NotImplementedException();

        public async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            var message = (msgOrCallback as MessageEventArgs).Message;

            var client = BotWorker.Get();
            await client.SendTextMessageAsync(message.Chat.Id, "Нет такой команды, товарищ!");
        }
    }
}
