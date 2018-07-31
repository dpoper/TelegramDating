using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.StateMachine
{
    public abstract class State
    {
        public abstract Task Handle(User currentUser, EventArgs msgOrCallback);
    }
}
