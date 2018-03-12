using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            if (Database.ContainsUser(message.Chat.Username))
            {
                await client.SendTextMessageAsync(message.Chat.Id, "толян ты ебанулся чтоли, ты уже в базе");
                return;
            }

            var currentUser = new User(message.From.Id, message.From.Username);
            Database.AddUser(currentUser);

            await client.SendTextMessageAsync(message.Chat.Id, "Привет! Как мне к тебе обращаться?");
            
        }
    }
}
