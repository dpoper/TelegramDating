using TelegramDating.Database;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands
{
    public abstract class Command
    {
        public UserContext UserContext { get; set;  }

        public BotWorker BotWorker { get; set; }

        public abstract void Execute(User currentUser, string @params = "");
    }
}
