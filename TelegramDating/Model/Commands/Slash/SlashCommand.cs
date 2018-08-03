using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramDating.Model.Commands.Slash
{
    public interface ISlashCommand : ICommand
    {
        string SlashText { get; }
    }
}
