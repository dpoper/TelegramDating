using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;

namespace TelegramDating.Model
{
    public static partial class BotWorker
    {
        private static readonly UserRepository userRepository = UserRepository.Initialize();

        /// <summary>
        /// Master method for message handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        private static async void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Console.WriteLine(message.MessageId);

            if (message.Type == MessageType.Text && message.Text[0] == '/') await ExecuteCommand(message);
            else await ExecuteMessageText(messageEventArgs);
        }

        private static async Task ExecuteCommand(Message message)
        {
            var command = Commands.SingleOrDefault(cmd => message.Text == cmd.Name);
            await command.Execute(message);
            Console.WriteLine($"Команда: {message.Text} | {message.Chat.Username}");
        }

        private static async Task ExecuteMessageText(MessageEventArgs messageEventArgs)
        {
            User currentUser = userRepository.Get(messageEventArgs.Message.Chat.Id);
            await currentUser.HandleState(messageEventArgs);
        }

        private static async void HandleCallbackQuery(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            var Callback = queryEventArgs.CallbackQuery;
            User currentUser = userRepository.Get(Callback.From.Id);

            if (!(currentUser.State is StateMachine.IGotCallbackQuery))
            {
                await currentUser.HandleState(queryEventArgs);
                return;
            }
        }
    }

}

