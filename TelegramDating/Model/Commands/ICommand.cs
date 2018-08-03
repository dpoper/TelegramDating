using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramDating.Model.Commands
{
    public interface ICommand
    {
        Task Execute(User currentUser, EventArgs msgOrCallback);
    }
}
