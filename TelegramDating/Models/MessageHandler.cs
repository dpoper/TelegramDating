using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramDating.Models
{
    public static partial class Bot
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

            if (Message.Text[0] == '/') await ExecuteCommand(Client, Message);
            else await ExecuteText(Client, Message);
        }

        private static async Task ExecuteCommand(TelegramBotClient Client, Message Message)
        {
            var command = Commands.SingleOrDefault(cmd => Message.Text == cmd.Name);
            await command.Execute(Message, Client);
            Console.WriteLine($"Команда: {Message.Text} | {Message.Chat.Username}");

            //await new NoCommand().Execute(Message, Client);
            // Console.WriteLine($"Команда: {Message.Text} | {Message.Chat.Username} | (не найдена)");
        }

        private static async Task ExecuteText(TelegramBotClient Client, Message Message)
        {
            User currentUser = Database.Get(Message.Chat.Id);
            if (currentUser is null) throw new Exception("Пизда.");

            switch (currentUser.State)
            {
                case (int)State.Create.Name:
                    currentUser.Name = Message.Text;
                    currentUser.State = (int)State.Create.Age;
                    Database.Submit();

                    await Client.SendTextMessageAsync(Message.Chat.Id, "Классное имя, наверное! А сколько тебе лет?");
                    break;

                case (int)State.Create.Age:
                    int Age;
                    if (int.TryParse(Message.Text, out Age) && Age > 0)
                    {
                        currentUser.Age = Age;
                        currentUser.State = (int)State.Create.City;
                        Database.Submit();

                        await Client.SendTextMessageAsync(Message.Chat.Id, "Ого! совсем уже взрослый. А ты откуда?");
                    }
                    else
                    {
                        await Client.SendTextMessageAsync(Message.Chat.Id, "ты ахуел бля");
                    }
                    break;

                case (int)State.Create.City:
                    // ..
                    break;
            }
        }
    }
}
