using System.Threading.Tasks;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands
{
    public abstract class Command
    {
        public UserContext UserContext { get; } = Container.Current.Resolve<UserContext>();

        public abstract void Execute(User currentUser, string @params = "");
    }
}
