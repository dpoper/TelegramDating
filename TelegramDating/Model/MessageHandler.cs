using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;
using TelegramDating.Model.Commands.Slash;

namespace TelegramDating.Model
{
    public static class MessageHandler
    {
        private static readonly UserRepository userRepository = UserRepository.Initialize();

        /// <summary>
        /// Master method for message handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        public static async void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Console.WriteLine(message.MessageId);

            if (message.Type == MessageType.Text && message.Text[0] == '/')
                await ExecuteCommand(messageEventArgs);
            else
                await HandleMessageText(messageEventArgs);
        }

         static async Task ExecuteCommand(MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            var command = BotWorker.FindSlashCommand(message.Text);

            if (command == null)
            {
                await new NoCommand().Execute(new User(), messageEventArgs);
                return;
            }

            await command?.Execute(new User(), messageEventArgs);
            Console.WriteLine($"Команда: {message.Text} | {message.Chat.Username}");
        }

        private static async Task HandleMessageText(MessageEventArgs messageEventArgs)
        {
            User currentUser = userRepository.Get(messageEventArgs.Message.Chat.Id);
            await currentUser.HandleAction(messageEventArgs);
        }

        public static async void HandleCallbackQuery(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            var Callback = queryEventArgs.CallbackQuery;
            User currentUser = userRepository.Get(Callback.From.Id);

            var currentAction = BotWorker.FindAction(currentUser.ChatActionId);

            if (!(currentAction is Commands.Bot.IGotCallbackQuery))
                return;

            await currentUser.HandleAction(queryEventArgs);
        }
    }

}

