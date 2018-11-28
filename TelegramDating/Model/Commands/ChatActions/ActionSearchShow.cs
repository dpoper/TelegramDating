using System;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.ChatActions
{
    internal class ActionSearchShow : ChatAction
    {
        public override int Id => 8;
        
        public override Type NextAction => null;

        public override Task Execute(User currentUser, EventArgs msgOrCallback) => this.HandleResponse(currentUser, msgOrCallback);

        protected override async Task HandleResponse(User currentUser, EventArgs msgOrCallback)
        {
            Console.WriteLine($"{currentUser.Name} очень хочет найти кого-то, быстрее создай экшн!");
        }

        protected override async Task HandleAfter(User currentUser, EventArgs msgOrCallback)
        {
        }
    }
}