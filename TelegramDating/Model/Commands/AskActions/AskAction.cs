using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramDating.Model.Commands.AskActions
{
    public abstract class AskAction
    {
        public abstract int Id { get; }

        public abstract void Ask(User currentUser);

        public virtual void OnSuccess(User currentUser, CallbackQuery cquery = null, Message message = null) { }

        public virtual bool Validate(User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return AskAction.BaseTextValidation(cquery, message);
        }

        public virtual void OnValidationFail(User currentUser) { }

        public static bool BaseTextValidation(CallbackQuery cquery = null, Message message = null)
        {
            return cquery != null || message != null
                                  && message.Type == MessageType.Text;
        }
    }
}