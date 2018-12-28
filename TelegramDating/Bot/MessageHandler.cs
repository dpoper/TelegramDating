using System;
using System.Linq;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramDating.Extensions;
using TelegramDating.Bot.Commands.AskActions;
using TelegramDating.Bot.Commands.Slash;
using TelegramDating.Enums;
using TelegramDating.Model;

namespace TelegramDating.Bot
{
    public partial class BotWorker
    {
        internal void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.ToMessage();

            if (string.IsNullOrEmpty(message.From.Username))
            {
                this.SendNoUsernameSetMessage(message.From.Id);
                return;
            }

            Console.WriteLine($"Message: {message.MessageId} | {message.From.Username} | {message.Text}");

            User currentUser = this.UserContext.GetByUserId(message.Chat.Id);

            if (currentUser == null)
            {
                this.StartNewUser(message.From.Id, message.From.Username);
                return;
            }

            currentUser.LastVisitAt = DateTime.Now;
            this.UserContext.SaveChanges();

            if (message.Type == MessageType.Text && message.Text.Length > 0 && message.Text[0] == '/')
            {
                this.ExecuteAsCommand(currentUser, message.Text);
                return;
            }

            if (currentUser.IsCreatingProfile()) 
            {
                this.CreatingProfileLoop(currentUser, message: message);
                return;
            }

            if (!this.TrySendNextProfileForResponse(currentUser))
                this.TrySendNextProfile(currentUser);
        }

        internal void HandleCallbackQuery(object sender, CallbackQueryEventArgs callbackArgs)
        {
            var callback = callbackArgs.ToCallbackQuery();
            this.RemoveKeyboard(callback);

            if (string.IsNullOrEmpty(callback.From.Username))
            {
                this.SendNoUsernameSetMessage(callback.From.Id);
                return;
            }

            Console.WriteLine($"Callback: {callback.Id} | {callback.From.Username} | {callback.Data}");

            User currentUser = this.UserContext.GetByUserId(callback.From.Id);

            if (currentUser == null)
            {
                this.StartNewUser(callback.From.Id, callback.From.Username);
                return;
            }

            currentUser.LastVisitAt = DateTime.Now;
            this.UserContext.SaveChanges();

            if (currentUser.IsCreatingProfile())
            {
                this.CreatingProfileLoop(currentUser, cquery: callback);
                return;
            }

            this.UserContext.LoadGotLikes(currentUser);

            // Response case
            var firstGotLike = currentUser.GotLikes.FirstOrDefault(x => x.Response == null);
            if (firstGotLike != null)
            {
                CallbackKeyboardExt.ExtractLike(callback.Data, firstGotLike);
                this.UserContext.SaveChanges();
            }
            else // Request case
            {
                var like = CallbackKeyboardExt.ExtractLike(callback.Data);
                if (!currentUser.GotLikes.Select(x => x.CheckedUser.Id).Contains(like.CheckedUser.Id))
                {
                    currentUser.Likes.Add(like);
                    this.UserContext.SaveChanges();
                }
            }
            
            if (!this.TrySendNextProfileForResponse(currentUser))
                this.TrySendNextProfile(currentUser);
        }

        private void ExecuteAsCommand(User currentUser, string slashText)
        {
            var command = this.FindSlashCommand(slashText);

            if (command is null)
            {
                new NoCommand().Execute(currentUser);
                Console.WriteLine($"Command: {slashText} | {currentUser.Username} | Нет");
                return;
            }

            command.Execute(currentUser, slashText);
            Console.WriteLine($"Command: {slashText} | {currentUser.Username}");
        }

        private async void CreatingProfileLoop(User currentUser, Telegram.Bot.Types.Message message = null, Telegram.Bot.Types.CallbackQuery cquery = null)
        {
            AskAction currentAsk = this.FindAskAction(currentUser.ProfileCreatingState.Value);

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

            bool isValid = currentAsk.Validate(currentUser, cquery: cquery, message: message);

            if (!isValid)
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
                        try
                        {
                            Telegram.Bot.Types.Message messageWithPhoto = await this.Instance.SendPhotoAsync(currentUser.UserId, message.Text);
                            currentUser.SetInfo(messageWithPhoto.Photo.Last());
                        }
                        catch
                        {
                            await this.Instance.SendTextMessageAsync(currentUser.UserId, "Что-то пошло не так. Кажется, ссылка битая.");
                        }

                        return;
                    }

                    currentUser.SetInfo(message.Text);
                }
                else if (message.Type == MessageType.Photo && currentAsk is AskPicture)
                {
                    currentUser.SetInfo(message.Photo.Last());
                }
            }

            currentAsk.OnSuccess(currentUser, message: message, cquery: cquery);

            AskAction nextAsk = this.GetNextAskAction(currentUser.ProfileCreatingState.Value);

            if (nextAsk != null)
            {
                currentUser.ProfileCreatingState = (ProfileCreatingEnum?) nextAsk.Id;
                nextAsk.Ask(currentUser);
            }
            else
            {
                await this.Instance.SendTextMessageAsync(currentUser.UserId,
                    "Ура, ты зарегистрировался! Теперь мы попробуем найти кого-нибудь для тебя.");

                this.TrySendNextProfile(currentUser);
                currentUser.ProfileCreatingState = null;
            }

            this.UserContext.SaveChanges();
        }
    }
}

