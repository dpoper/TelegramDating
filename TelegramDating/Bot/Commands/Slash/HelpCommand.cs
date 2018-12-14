using System.ComponentModel;
using System.Text;
using TelegramDating.Extensions;
using TelegramDating.Model;

namespace TelegramDating.Bot.Commands.Slash
{
    [Description("показать это окно :)")]
    internal class HelpCommand : SlashCommand
    {
        public override string SlashText { get; } = "/help";

        public override async void Execute(User currentUser, string @params = "")
        {
            var helpSb = new StringBuilder("Вот список доступных для тебя команд:\n\n"); 

            foreach (SlashCommand cmd in BotWorker.AvailableSlashCommandList)
                helpSb.Append($"{cmd.SlashText} - {cmd.GetDescription()}\n");

            await Program.Bot.SendTextMessageAsync(currentUser.UserId, helpSb.ToString().Trim());
        }
    }
}
