using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramDating.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        public override async Task Execute(Message Message, TelegramBotClient Client)
        {
            if (Database.ContainsUser(Message.Chat.Username))
            {
                await Client.SendTextMessageAsync(Message.Chat.Id, "толян ты ебанулся чтоли, ты уже в базе");

                // new HelpCommand.Execute();
                return;
            }

            var currentUser = new User(Message.From.Id, Message.From.Username);
            Database.AddUser(currentUser);

            await Client.SendTextMessageAsync(Message.Chat.Id, "Привет! Как мне к тебе обращаться?");
            
        }
    }
}
