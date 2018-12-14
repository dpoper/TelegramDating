using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramDating.Bot.Commands.AskActions
{
    public abstract class AskAction
    {
        public abstract int Id { get; }

        public abstract void Ask(Model.User currentUser);

        public virtual void OnSuccess(Model.User currentUser, CallbackQuery cquery = null, Message message = null) { }

        public virtual bool Validate(Model.User currentUser, CallbackQuery cquery = null, Message message = null)
        {
            return AskAction.BaseTextValidation(cquery, message);
        }

        public virtual void OnValidationFail(Model.User currentUser) { }

        public static bool BaseTextValidation(CallbackQuery cquery = null, Message message = null)
        {
            return cquery != null || message != null
                                  && message.Type == MessageType.Text;
        }
    }
}