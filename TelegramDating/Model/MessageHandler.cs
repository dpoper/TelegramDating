using System;
using System.Linq;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;
using TelegramDating.Model;
using TelegramDating.Model.Commands.AskActions;
using TelegramDating.Model.Commands.Slash;
using TelegramDating.Model.Enums;

namespace TelegramDating
{
    public static class MessageHandler
    {
        private static UserContext UserContext { get; set; } = Container.Current.Resolve<UserContext>();

        internal static void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.ToMessage();

            Console.WriteLine($"Message: {message.MessageId} | {message.From.Username} | {message.Text}");

            User currentUser = UserContext.GetByUserId(message.Chat.Id);

            if (currentUser == null)
            {
                currentUser = new User(message.From.Id, message.From.Username);
                BotWorker.FindSlashCommand("/start").Execute(currentUser);
                return;
            }

            if (message.Type == MessageType.Text && message.Text[0] == '/')
            {
                var command = BotWorker.FindSlashCommand(message.Text);

                if (command == null)
                {
                    new NoCommand().Execute(currentUser);
                    Console.WriteLine($"Command: {message.Text} | {message.Chat.Username} | Нет");
                    return;
                }

                command.Execute(currentUser, message.Text);
                Console.WriteLine($"Command: {message.Text} | {message.Chat.Username}");

                return;
            }

            if (currentUser.IsCreatingProfile()) // needs null/empty checks
            {
                AskAction currentAsk = BotWorker.FindAskAction(currentUser.ProfileCreatingState.Value);

                if (currentUser.ProfileCreatingState == ProfileCreatingEnum.Picture)
                    currentUser.SetInfo(message.Photo.Last().FileId);
                else
                    currentUser.SetInfo(message.Text);

                currentAsk.After(currentUser, message: message);

                AskAction nextAsk = BotWorker.GetNextAskAction(currentUser.ProfileCreatingState.Value);

                if (nextAsk != null)
                {
                    currentUser.ProfileCreatingState = (ProfileCreatingEnum?) nextAsk.Id;
                    nextAsk.Ask(currentUser);
                }
                else
                {
                    currentUser.ProfileCreatingState = null;
                }

                UserContext.SaveChanges();
            }
        }


        internal static void HandleCallbackQuery(object sender, CallbackQueryEventArgs callbackArgs)
        {
            var callback = callbackArgs.ToCallbackQuery();

            Console.WriteLine($"Callback: {callback.Id} | {callback.From.Username} | {callback.Data}");

            User currentUser = UserContext.GetByUserId(callback.From.Id);

            if (currentUser == null)
            {
                currentUser = new User(callback.From.Id, callback.From.Username);
                BotWorker.FindSlashCommand("/start").Execute(currentUser);
                return;
            }

            if (currentUser.IsCreatingProfile()) // needs null/empty checks
            {
                AskAction currentAsk = BotWorker.FindAskAction(currentUser.ProfileCreatingState.Value);
                currentUser.SetInfo(callback.Data);
                currentAsk.After(currentUser, cquery: callback);

                AskAction nextAsk = BotWorker.GetNextAskAction(currentUser.ProfileCreatingState.Value);

                if (nextAsk == null)
                {
                    Console.WriteLine("Search!!!!!!!!!!!!!!!!!!!");
                    return;
                }

                currentUser.ProfileCreatingState = (ProfileCreatingEnum?)nextAsk.Id;
                nextAsk.Ask(currentUser);

                return;
            }
        }
    }

}

