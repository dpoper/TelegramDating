using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.Bot
{
    internal class ActionSearchShow : IChatAction
    {
        public int Id => 8;

        public async Task Execute(User currentUser, EventArgs msgOrCallback)
        {
            Console.WriteLine("Ну пока пусто такое!");
        }
    }
}