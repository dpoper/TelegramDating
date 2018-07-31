using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.StateMachine
{
    internal class StateSearchShow : State
    {
        public override async Task Handle(User currentUser, EventArgs msgOrCallback)
        {
            throw new NotImplementedException();
        }
    }
}