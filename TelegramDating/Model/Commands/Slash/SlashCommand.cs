
namespace TelegramDating.Model.Commands.Slash
{
    /// <summary>
    /// Command that starts with the slash ('/') character.
    /// </summary>
    public abstract class SlashCommand : Command
    {
        public abstract string SlashText { get; }
    }
}
