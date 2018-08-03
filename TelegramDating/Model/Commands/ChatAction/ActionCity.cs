using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionCity : IChatAction
    {
        public int Id => 5;

        public async Task Execute(User currentUser, EventArgs msgEvArgs)
        {
            var client = BotWorker.Get();
            var userRepo = UserRepository.Initialize();

            var message = (msgEvArgs as MessageEventArgs)?.Message;
            if (message == null) return;

            currentUser.City = message.Text;
            
            await client.SendTextMessageAsync(message.Chat.Id, 
                "Отлично! Последний штрих – твоё фото. Пришли мне картинку.");

            currentUser.ChatActionId = new ActionPicture().Id;
            userRepo.Submit();
        }
    }
}

