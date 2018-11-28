using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;
using TelegramDating.Model;
using TelegramDating.Model.Commands.ChatActions;
using TelegramDating.Model.Commands.Slash;
using TelegramDating.Model.Enums;

namespace TelegramDating
{
    public static class MessageHandler
    {
        public static UserContext DbContext { get; set; } = Container.Current.Resolve<UserContext>();

        /// <summary>
        /// Master method for message handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        public static async void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Console.WriteLine(message.MessageId);

            User currentUser = DbContext.Users.SingleOrDefault(u => u.Id == messageEventArgs.Message.Chat.Id);

            if (currentUser == null)
                await BotWorker.FindSlashCommand("/start").Execute(null, messageEventArgs);

            if (message.Type == MessageType.Text && message.Text[0] == '/')
                await ExecuteCommand(messageEventArgs);
            else
                await HandleMessageText(messageEventArgs);
        }

        private static async Task ExecuteCommand(MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            var command = BotWorker.FindSlashCommand(message.Text);

            if (command == null)
            {
                await new NoCommand().Execute(new User(), messageEventArgs);
                return;
            }

            await command.Execute(new User(), messageEventArgs);
            Console.WriteLine($"Команда: {message.Text} | {message.Chat.Username}");
        }

        private static async Task HandleMessageText(MessageEventArgs messageEventArgs)
        {
            User currentUser = DbContext.Users.SingleOrDefault(u => u.Id == messageEventArgs.Message.Chat.Id);
            await currentUser?.HandleAction(messageEventArgs);
        }

        public static async void HandleCallbackQuery(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            var callback = queryEventArgs.CallbackQuery;
            User currentUser = DbContext.Users.SingleOrDefault(u => u.UserId == callback.From.Id);

            if (currentUser == null || !((ChatActionEnum)currentUser.ChatActionId).IsCallbackQueryAction() )
                return;

            var currentAction = BotWorker.FindAction(currentUser.ChatActionId);

            if (!(currentAction is IGotCallbackQuery))
                return;

            await currentUser.HandleAction(queryEventArgs);
        }
    }

}

