using TelegramDating.Database;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands
{
    public abstract class Command
    {
        public UserContext UserContext { get; } = Container.Current.Resolve<UserContext>();

        public abstract void Execute(User currentUser, string @params = "");
    }
}
