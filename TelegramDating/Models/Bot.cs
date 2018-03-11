using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TelegramDating.Models;
using TelegramDating.Models.Commands;

namespace TelegramDating.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandList;
        
        public static IReadOnlyList<Command> Commands { get => commandList.AsReadOnly(); }
             
        public static async Task<TelegramBotClient> Get()
        {
            if (client != null) return client;

            client = new TelegramBotClient(Program.Token);

            commandList = new List<Command>() {
                new StartCommand(),
                new ResetCommand()
            };


            // await client.SetWebhookAsync();
            client.StartReceiving();
            client.OnMessage += ExecuteCommands;

            return client;

        }

        public static async void ExecuteCommands(object sender, MessageEventArgs messageEventArgs)
        {
            var Client = sender as TelegramBotClient;
            var Message = messageEventArgs.Message;

            foreach (var cmd in Commands)
            {
                if (cmd.Contains(Message.Text))
                {
                    await cmd.Execute(Message, Client);
                    Console.WriteLine($"Команда: {cmd.Name} | {Message.Chat.Username}");
                    return;
                }
            }

            await new NoCommand().Execute(Message, Client);
            Console.WriteLine($"Команда: ----- | {Message.Chat.Username}");
        }

    }
}
