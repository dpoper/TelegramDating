using System;
using System.Linq;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramDating.Database;
using TelegramDating.Extensions;
using TelegramDating.Bot.Commands.AskActions;
using TelegramDating.Bot.Commands.Slash;
using TelegramDating.Enums;
using TelegramDating.Model;

namespace TelegramDating.Bot
{
    public static class MessageHandler
    {
        private static UserContext UserContext { get; set; } = Container.Current.Resolve<UserContext>();

        internal static void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.ToMessage();

            if (string.IsNullOrEmpty(message.From.Username))
            {
                BotWorker.SendNoUsernameSetMessage(message.From.Id);
                return;
            }

            Console.WriteLine($"Message: {message.MessageId} | {message.From.Username} | {message.Text}");

            User currentUser = UserContext.GetByUserId(message.Chat.Id);

            if (currentUser == null)
            {
                BotWorker.StartNewUser(message.From.Id, message.From.Username);
                return;
            }

            if (message.Type == MessageType.Text && message.Text.Length > 0 && message.Text[0] == '/')
            {
                MessageHandler.ExecuteAsCommand(currentUser, message.Text);
                return;
            }

            if (currentUser.IsCreatingProfile()) 
            {
                MessageHandler.CreatingProfileLoop(currentUser, message: message);
                return;
            }
            
            BotWorker.SendNextProfileForLike(currentUser);
        }

        internal static async void HandleCallbackQuery(object sender, CallbackQueryEventArgs callbackArgs)
        {
            var callback = callbackArgs.ToCallbackQuery();

            if (string.IsNullOrEmpty(callback.From.Username))
            {
                BotWorker.SendNoUsernameSetMessage(callback.From.Id);
                return;
            }

            Console.WriteLine($"Callback: {callback.Id} | {callback.From.Username} | {callback.Data}");

            User currentUser = UserContext.GetByUserId(callback.From.Id);

            if (currentUser == null)
            {
                BotWorker.StartNewUser(callback.From.Id, callback.From.Username);
                return;
            }

            if (currentUser.IsCreatingProfile())
            {
                MessageHandler.CreatingProfileLoop(currentUser, cquery: callback);
                return;
            }

            var like = CallbackKeyboardExt.ExtractLike(currentUser, callback.Data);

            if (!currentUser.Likes.Select(x => x.CheckedUser.Id).Contains(like.CheckedUser.Id))
            {
                currentUser.Likes.Add(like);
                UserContext.SaveChanges();
            }

            BotWorker.RemoveKeyboard(callback);
            BotWorker.SendNextProfileForLike(currentUser);
        }

        private static void ExecuteAsCommand(User currentUser, string slashText)
        {
            var command = BotWorker.FindSlashCommand(slashText);

            if (command is null)
            {
                new NoCommand().Execute(currentUser);
                Console.WriteLine($"Command: {slashText} | {currentUser.Username} | Нет");
                return;
            }

            command.Execute(currentUser, slashText);
            Console.WriteLine($"Command: {slashText} | {currentUser.Username}");
        }

        private static async void CreatingProfileLoop(User currentUser, Telegram.Bot.Types.Message message = null, Telegram.Bot.Types.CallbackQuery cquery = null)
        {
            AskAction currentAsk = BotWorker.FindAskAction(currentUser.ProfileCreatingState.Value);

            if (currentAsk is IGotCallbackQuery)
            {
                if (cquery == null)
                    return;
            }
            else
            {
                if (message == null)
                    return;
            }

            bool isValidated = currentAsk.Validate(currentUser, cquery: cquery, message: message);

            if (!isValidated)
            {
                currentAsk.OnValidationFail(currentUser);
                return;
            }

            if (cquery != null) currentUser.SetInfo(cquery.Data);
            else if (message != null)
            {
                if (message.Type == MessageType.Text)
                {
                    if (currentAsk is AskPicture)
                    {
                        Telegram.Bot.Types.Message messageWithPhoto;

                        try
                        {
                            messageWithPhoto = await Program.Bot.SendPhotoAsync(currentUser.UserId, message.Text);
                        }
                        catch
                        {
                            await Program.Bot.SendTextMessageAsync(currentUser.UserId, "Что-то пошло не так. Кажется, ссылка битая.");
                            return;
                        }

                        currentUser.SetInfo(messageWithPhoto.Photo.Last());
                    }
                    else
                    { 
                        currentUser.SetInfo(message.Text);
                    }
                }
                else if (message.Type == MessageType.Photo && currentAsk is AskPicture)
                {
                    currentUser.SetInfo(message.Photo.Last());
                }
            }

            currentAsk.OnSuccess(currentUser, message: message, cquery: cquery);

            AskAction nextAsk = BotWorker.GetNextAskAction(currentUser.ProfileCreatingState.Value);

            if (nextAsk != null)
            {
                currentUser.ProfileCreatingState = (ProfileCreatingEnum?) nextAsk.Id;
                nextAsk.Ask(currentUser);
            }
            else
            {
                await Program.Bot.SendTextMessageAsync(currentUser.UserId,
                    "Ура, ты зарегистрировался! Теперь мы попробуем найти кого-нибудь для тебя.");

                BotWorker.SendNextProfileForLike(currentUser);
                currentUser.ProfileCreatingState = null;
            }

            UserContext.SaveChanges();
        }
    }
}

