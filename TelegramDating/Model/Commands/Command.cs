using System;
using System.Threading.Tasks;
using TelegramDating.Database;

namespace TelegramDating.Model.Commands
{
    public abstract class Command
    {
        public UserContext DbContext { get; } = Container.Current.Resolve<UserContext>();

        public abstract Task Execute(User currentUser, EventArgs msgOrCallback);
    }
}
