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

            this.SendNextProfileForLike(currentUser);
        }

        internal void HandleCallbackQuery(object sender, CallbackQueryEventArgs callbackArgs)
        {
            var callback = callbackArgs.ToCallbackQuery();
            
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

            var like = CallbackKeyboardExt.ExtractLike(callback.Data);

            if (!this.UserContext.LoadGotLikes(currentUser).Select(x => x.CheckedUser.Id).Contains(like.CheckedUser.Id))
            {
                currentUser.Likes.Add(like);
                this.UserContext.SaveChanges();
            }

            this.RemoveKeyboard(callback);
            this.SendNextProfileForLike(currentUser);
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
                            messageWithPhoto = await this.Instance.SendPhotoAsync(currentUser.UserId, message.Text);
                        }
                        catch
                        {
                            await this.Instance.SendTextMessageAsync(currentUser.UserId, "Что-то пошло не так. Кажется, ссылка битая.");
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

                this.SendNextProfileForLike(currentUser);
                currentUser.ProfileCreatingState = null;
            }

            this.UserContext.SaveChanges();
        }
    }
}

