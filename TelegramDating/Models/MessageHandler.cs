using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramDating.Models
{
    public static partial class BotWorker
    {
        /// <summary>
        /// Master method for message handling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageEventArgs"></param>
        private static async void HandleMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var Client = sender as TelegramBotClient;
            var Message = messageEventArgs.Message;

            if (Message.Type == MessageType.TextMessage && Message.Text[0] == '/') await ExecuteCommand(Client, Message);
            else await ExecuteOther(Client, Message);
        }

        private static async Task ExecuteCommand(TelegramBotClient Client, Message Message)
        {
            var command = Commands.SingleOrDefault(cmd => Message.Text == cmd.Name);
            await command.Execute(Message, Client);
            Console.WriteLine($"Команда: {Message.Text} | {Message.Chat.Username}");

            //await new NoCommand().Execute(Message, Client);
            // Console.WriteLine($"Команда: {Message.Text} | {Message.Chat.Username} | (не найдена)");
        }

        private static async Task ExecuteOther(TelegramBotClient Client, Message Message)
        {
            User currentUser = Database.Get(Message.Chat.Id);

            switch (currentUser.State)
            {
                case (int)State.Create.Name:
                    {
                        currentUser.Name = Message.Text;
                        currentUser.State = (int)State.Create.Age;
                        Database.Submit();

                        await Client.SendTextMessageAsync(Message.Chat.Id, 
                            $"Приятно познакомиться, {currentUser.Name}! " +
                            $"А сколько тебе лет?");

                        break;
                    }
                case (int)State.Create.Age:
                    {
                        int Age;
                        if (int.TryParse(Message.Text, out Age) && Age > 0)
                        {
                            currentUser.Age = Age;
                            currentUser.State = (int)State.Create.Country;
                            Database.Submit();

                            await Client.SendTextMessageAsync(Message.Chat.Id, "Ого! совсем уже взрослый. А ты из какой страны?");
                        }
                        else
                        {
                            await Client.SendTextMessageAsync(Message.Chat.Id, "ты ахуел бля");
                        }
                        break;
                    }
                case (int)State.Create.Country:
                    {
                        currentUser.Country = Message.Text;
                        currentUser.State = (int)State.Create.City;
                        Database.Submit();

                        await Client.SendTextMessageAsync(Message.Chat.Id, "Хорошо! А город?");
                        break;
                    }
                case (int)State.Create.City:
                    {
                        currentUser.City = Message.Text;
                        currentUser.State = (int)State.Create.Picture;
                        Database.Submit();

                        await Client.SendTextMessageAsync(Message.Chat.Id, "Отлично! Последний штрих – твоё фото. Пришли мне картинку.");
                        break;
                    }
                case (int)State.Create.Picture:
                    {
                        if (Message.Type != MessageType.PhotoMessage)
                        {
                            await Client.SendTextMessageAsync(Message.Chat.Id, "Ебло пришли фотку");
                            return;
                        }

                        // var link = string.Format(Bot.FileURL, Program.Token, photo.FilePath);
                        // Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", link);

                        string pictureId = Message.Photo.Last().FileId;
                        // var photo = await Client.GetFileAsync(pictureId);
                        currentUser.PictureId = pictureId;
                        currentUser.State = (int)State.Create.SearchSex;

                        await Client.SendTextMessageAsync(Message.Chat.Id, "Это ты? Красивый...");
                        

                        var sexKeyboard = new InlineKeyboardMarkup(new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Мальчика"),
                            InlineKeyboardButton.WithCallbackData("Девочку"),
                            InlineKeyboardButton.WithCallbackData("Без разницы")
                        });

                        await Client.SendTextMessageAsync(
                                   Message.Chat.Id,
                                   "Так, а кого искать тебе будем?",
                                   replyMarkup: sexKeyboard
                                    );
                        break;
                    }
            }
        }



        private static async void HandleCallbackQuery(object sender, CallbackQueryEventArgs queryEventArgs)
        {
            var Client = sender as TelegramBotClient;
            var Callback = queryEventArgs.CallbackQuery;

            User currentUser = Database.Get(Callback.From.Id);

            Console.WriteLine(currentUser.State);

            switch (currentUser.State)
            {
                case (int)State.Create.Sex:
                    {
                        if (Callback.Data == "Мальчик") currentUser.Sex = true;
                        else currentUser.Sex = false;

                        Console.WriteLine(currentUser.Sex);
                        currentUser.State = (int)State.Create.Name;
                        Database.Submit();

                        await Client.SendTextMessageAsync(Callback.From.Id, "Хорошо! А как тебя зовут?");

                        break;
                    }
                case (int)State.Create.SearchSex:
                    {
                        if (Callback.Data == "Мальчика")    currentUser.SearchSex = (int) SearchOptions.Sex.Male;
                        if (Callback.Data == "Девочку")     currentUser.SearchSex = (int) SearchOptions.Sex.Female;
                        if (Callback.Data == "Без разницы") currentUser.SearchSex = (int) SearchOptions.Sex.Any;

                        currentUser.State = (int)State.Search.Show;
                        Database.Submit();

                        await Client.SendTextMessageAsync(Callback.From.Id,
                            $"Чудесно, {currentUser.Name}! " +
                            $"Теперь перейдём к поиску :)");


                        //
                        await Client.SendTextMessageAsync(Callback.From.Id,
                            "*типа поисковая панель и анкета рандом юзера с фоткой*");

                        break;
                    }

            }
        }
    }
}
