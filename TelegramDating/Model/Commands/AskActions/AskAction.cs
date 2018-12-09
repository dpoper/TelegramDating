
namespace TelegramDating.Model.Commands.AskActions
{
    public abstract class AskAction
    {
        public abstract int Id { get; }

        public abstract void Ask(User currentUser);

        public virtual void After(User currentUser) { }
    }
}